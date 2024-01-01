
namespace BlImplementation;
using BlApi;
using BO;
using DalApi;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using System.Xml.Linq;
internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    #region private help methods
    /// <summary>
    /// Create milstones according to the dependencies
    /// </summary>
    /// <param name="dependencies"></param>
    /// <returns>List of new dependencies</returns>
    private List<DO.Dependency> createMilestones(List<DO.Dependency?> dependencies)
    {
        List<DO.Task?> oldTasks = _dal.Task.ReadAll().ToList();//read the tasks and group them by DependentTask
        var groupDependencies = (from dep in dependencies
                                 where dep.DependentTask is not null && dep.DependsOnTask is not null
                                 group dep by dep.DependentTask into gropByDependentTask
                                 let depList = (from dep in gropByDependentTask
                                                select dep.DependsOnTask).Order()
                                 select new { _key = gropByDependentTask.Key, _value = depList });
        var listAfterDistinct = (from dep in groupDependencies//list without duplicates
                                 select dep._value.ToList()).Distinct(new BO.Tools.DistinctIntList()).ToList();

        List<DO.Dependency> newDepsList = new List<DO.Dependency>();
        int i = 1;
        foreach (var groupOfDepentOnTasks in listAfterDistinct)
        {   //create milstone for each one
            DO.Task milestone = new DO.Task(-1, "I'm a milstone :)", $"M{i}", true, DateTime.Now, new TimeSpan(0), null, null, null, null, null, null, null, null);
            int idMilestone = _dal.Task.Create(milestone);

            foreach (var taskListwithDeps in groupDependencies)
            {   //set each task that depent on this milestone
                var t = taskListwithDeps._value.ToList();
                if (t.SequenceEqual(groupOfDepentOnTasks))
                    newDepsList.Add(new DO.Dependency(-1, taskListwithDeps._key!.Value, idMilestone));
            }

            foreach (var dep in groupOfDepentOnTasks)
            {   //set each task that this milestone depent on it
                newDepsList.Add(new DO.Dependency(-1, idMilestone, dep));
            }
            i++;
        }

        //Start

        //create milestone of start
        DO.Task startMilestoneTask = new DO.Task(-1, "description", "start", true, DateTime.Now, null, null, null, null, null, null, null, null, null);
        int idStartMilstone = _dal.Task.Create(startMilestoneTask);
        newDepsList.Add(new DO.Dependency(-1, idStartMilstone, null));

        
        //find tasks that depent on start
        var notDepTasks = (from task in oldTasks
                           where !(from taskDep in groupDependencies
                                   select taskDep._key).Any(t => t == task.Id)
                           select task.Id);
        foreach (var task in notDepTasks)//create deps for tasks that depent on start
        {
            newDepsList.Add(new DO.Dependency(-1, task, idStartMilstone));
        }


        //End
        //create end milestone
        DO.Task endMilestoneTask = new DO.Task(-1, "description", "end", true, DateTime.Now, null, null, null, null, null, null, null, null, null);
        int idEndMilstone = _dal.Task.Create(endMilestoneTask);
        //find tasks that no task depend on them
        var endDepTasks = (from task in oldTasks
                           where !(from dep in dependencies
                                   select dep.DependsOnTask).Distinct().Any(t => t == task.Id)
                           select task.Id);
        foreach (var task in endDepTasks)//create dep for tasks that end depend on them
        {
            newDepsList.Add(new DO.Dependency(-1, idEndMilstone, task));
        }

        return newDepsList;
    }

    /// <summary>
    /// Recursive function to set deadline for each task
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="endMilestoneId"></param>
    /// <param name="depList"></param>
    /// <returns>the deadline for this task</returns>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    /// <exception cref="BO.BlInsufficientTime"></exception>
    private DateTime? updateDeadlines(int taskId, int endMilestoneId, List<DO.Dependency> depList)
    {
        if (taskId == endMilestoneId)//if this is the end milestone - stop.
            return _dal.EndProjectDate;
        DO.Task currentTask = _dal.Task.Read(taskId) ?? throw new BO.BlNullPropertyException($"Task with Id {taskId} does not exists");
        //get all the tasks that depent on this task
        List<int?> listOfDepentOnCurrentTask = (from dep in depList
                                                where dep.DependsOnTask == taskId
                                                select dep.DependentTask).ToList();

        DateTime? deadline = null;
        foreach (int task in listOfDepentOnCurrentTask)
        {
            DO.Task readTask = _dal.Task.Read(taskId)!;
            if (readTask.Deadline is null)//set deadline for each task that depent on me by calling the recursion
                readTask = readTask with { Deadline = updateDeadlines(task, endMilestoneId, depList) };
            if (deadline is null || readTask.Deadline - readTask.RequiredEffortTime < deadline)//set my deadline
                deadline = readTask.Deadline - readTask.RequiredEffortTime;
        }
        if (deadline > _dal.EndProjectDate)//if there is not enough time for the project
            throw new BO.BlInsufficientTime("There is insufficient time to complete this task\n");
        currentTask = currentTask with { Deadline = deadline };//update the task with the deadline

        _dal.Task.Update(currentTask);
        return currentTask.Deadline;//return the deadline for the recursion
    }

    /// <summary>
    /// Recursive function to set scheduled dates for each task
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="startMilestoneId"></param>
    /// <param name="depList"></param>
    /// <returns>the scheduled date for this task</returns>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    /// <exception cref="BO.BlInsufficientTime"></exception>
    private DateTime? updateScheduledDates(int taskId, int startMilestoneId, List<DO.Dependency> depList)
    {
        if (taskId == startMilestoneId)//if this is the start milestone - stop.
            return _dal.StartProjectDate;
        DO.Task currentTask = _dal.Task.Read(taskId) ?? throw new BO.BlNullPropertyException($"Task with Id {taskId} does not exists");
        //get all the tasks that this task depent on them
        List<int?> listOfTasksThatCurrentDepsOnThem = (from dep in depList
                                                       where dep.DependentTask == taskId
                                                       select dep.DependsOnTask).ToList();

        DateTime? scheduledDate = null;
        foreach (int? task in listOfTasksThatCurrentDepsOnThem)
        {
            DO.Task readTask = _dal.Task.Read(taskId)!;
            if (readTask.ScheduledDate is null)//set scheduled date for each task that i depent on it by calling the recursion
                readTask = readTask with { ScheduledDate = updateDeadlines((int)task!, startMilestoneId, depList) };
            if (scheduledDate is null || readTask.ScheduledDate + readTask.RequiredEffortTime > scheduledDate)//set my scheduled date
                scheduledDate = readTask.ScheduledDate + readTask.RequiredEffortTime;
        }

        if (scheduledDate < _dal.StartProjectDate)//if there is not enough time for the project
            throw new BO.BlInsufficientTime("There is insufficient time to complete this task\n");
        currentTask = currentTask with { ScheduledDate = scheduledDate };//update the task with the scheduled date


        _dal.Task.Update(currentTask);
        return currentTask.ScheduledDate;//return the scheduled date for the recursion
    }
    #endregion
    /// <summary>
    /// Set milestones for the project.
    /// </summary>
    public void CreateProjectsSchedule()
    {
        List<DO.Dependency?> dependencies = _dal.Dependency.ReadAll().ToList();
        //List<DO.Dependency> newDepsList = createMilestones(dependencies);//get updated dependencies
        List<DO.Dependency> newDepsList = dependencies;
        _dal.Dependency.Reset();//clear the old dependencies
        foreach (var dep in newDepsList)
        {
            _dal.Dependency.Create(dep);//add each one to the file
        }


        List<DO.Task?> allTasks = _dal.Task.ReadAll().ToList();
        int startMilestoneId = allTasks.Where(task => task!.Alias == "start").Select(task => task!.Id).First();//get the milestone of start project

        DO.Task startMilestone = _dal.Task.Read(startMilestoneId)!;
        if (startMilestone is not null)
            startMilestone = startMilestone with { ScheduledDate = _dal.StartProjectDate };//set the start project date

        int endMilestoneId = allTasks.Where(task => task!.Alias == "end").Select(task => task!.Id).First();//get the milestone of end project

        DO.Task endMilestone = _dal.Task.Read(endMilestoneId)!;
        if (endMilestone is not null)
            endMilestone = endMilestone with { Deadline = _dal.EndProjectDate };//set the deadline project date

        startMilestone = startMilestone! with { Deadline = updateDeadlines(startMilestoneId, endMilestoneId, newDepsList) };
        _dal.Task.Update(startMilestone!);

        endMilestone = endMilestone! with { ScheduledDate = updateScheduledDates(endMilestoneId, startMilestoneId, newDepsList) };
        _dal.Task.Update(endMilestone);
    }
        
    /// <summary>
    /// Read a milestone
    /// </summary>
    /// <param name="id"></param>
    /// <returns>milestone</returns>
    /// <exception cref="BlDoesNotExistException"></exception>
    public BO.Milestone? Read(int id)
    {
        DO.Task milestoneFromDo = _dal.Task.Read(id) ?? throw new BlDoesNotExistException($"An object of type Milestone with ID {id} does not exist");
        if (milestoneFromDo.Milestone == false)
            return null;
        //calculating the forecast date
        DateTime? forecastDate = null;
        if (milestoneFromDo.Start is not null && milestoneFromDo.RequiredEffortTime is not null)
        {
            TimeSpan ts = milestoneFromDo.RequiredEffortTime ?? new TimeSpan(0);
            forecastDate = milestoneFromDo.Start?.Add(ts);
        }

        //calculating the completion percentage
        double? completionPercentage = ((DateTime.Now - milestoneFromDo.Start) / milestoneFromDo.RequiredEffortTime) * 100;
        if (completionPercentage > 100)
            completionPercentage = 100;

        //casting to BO entity of milestone
        BO.Milestone milestone = new()
        {
            Id = milestoneFromDo.Id,
            Description = milestoneFromDo.Description,
            Alias = milestoneFromDo.Alias,
            Status = (Status)(milestoneFromDo.ScheduledDate is null ? 0 ://set the status
                               milestoneFromDo.Start is null ? 1 :
                               milestoneFromDo.Complete is null ? 2
                               : 3),
            CreatedAtDate = milestoneFromDo.CreatedAt,
            ForecastDate = forecastDate,
            DeadlineDate = milestoneFromDo.Deadline,
            CompleteDate = milestoneFromDo.Complete,
            CompletionPercentage = completionPercentage,
            Remarks = milestoneFromDo.Remarks
        };
        return milestone;
    }

    /// <summary>
    /// Update a milestone
    /// </summary>
    /// <param name="m"></param>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Update(BO.Milestone m)
    {
        //convert to DO entity
        DO.Task doMilestone = new DO.Task(m.Id, m.Description, m.Alias, true, m.CreatedAtDate, new TimeSpan(0), null, m.ForecastDate, m.DeadlineDate, m.CompleteDate, null, m.Remarks, null, null);
        try
        {//call the DO update
            _dal.Task.Update(doMilestone);
        }
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"An object of type Task with ID {doMilestone.Id} does not exist", exception);
        }
    }

    /// <summary>
    /// Set dates for starting and ending the project
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public void setDates(DateTime start, DateTime end)
    {
        _dal.StartProjectDate= start;
        _dal.EndProjectDate= end;

        XElement? dataConfig = Tools.LoadListFromXMLElement("data-config");

        XElement xmlStart = new XElement("StartProjectDate", start);
        XElement xmlEnd = new XElement("EndProjectDate", end);

        dataConfig.Descendants("StartProjectDate").Remove();
        dataConfig.Descendants("EndProjectDate").Remove();

        dataConfig.Add(xmlStart);
        dataConfig.Add(xmlEnd);

        Tools.SaveListToXMLElement(dataConfig, "data-config");
    }
}
