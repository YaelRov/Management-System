
namespace BlImplementation;
using BlApi;
using BO;
using DalApi;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using System.Xml.Linq;

internal class MilestoneImplementation : IMilestone
{
    //================
 //    foreach (var tasks in listWithoutDuplicetes)
 //{
 //    List<int?> newList = new List<int?>();
 //   int milestoneId = _dal.Task.Create(new DO.Task(0, "milestone", "M" + runningNameForMilestone, DateTime.Now, TimeSpan.Zero, true));
 //    foreach (var task in tasks.Value)
 //    {
 //        newList.Add(task);
 //        int depToAdd = _dal.Dependence.Create(new DO.Dependence(0, milestoneId, task));
 //   listOfNewDependencies.Add(_dal.Dependence.Read(depToAdd)!);
 //    }

 //    foreach (var task in secondReadDep)
 //    {
 //        if (task.Value.SequenceEqual(newList))
 //            listOfNewDependencies.Add(new DO.Dependence(0, task._Key, milestoneId));
 //    }
 //    runningNameForMilestone++;
 //}
    //================

    private DalApi.IDal _dal = DalApi.Factory.Get;

    #region private help methods
    private List<DO.Dependency> createMilestones(List<DO.Dependency?> dependencies)
    {
        List<DO.Task?> oldTasks = _dal.Task.ReadAll().ToList();
        var groupDependencies = (from dep in dependencies
                                 where dep.DependentTask is not null && dep.DependsOnTask is not null
                                 group dep by dep.DependentTask into gropByDependentTask
                                 let depList = (from dep in gropByDependentTask
                                                select dep.DependsOnTask).Order()
                                 select new { _key = gropByDependentTask.Key, _value = depList });
        var listAfterDistinct = (from dep in groupDependencies
                                 select dep._value.ToList()).Distinct(new BO.Tools.DistinctIntList()).ToList();

        List<DO.Dependency> newDepsList = new List<DO.Dependency>();
        int i = 1;
        foreach (var groupOfDepentOnTasks in listAfterDistinct)
        {
            DO.Task milestone = new DO.Task(-1, "I'm a milstone :)", $"M{i}", true, DateTime.Now, new TimeSpan(0), null, null, null, null, null, null, null, null);
            int idMilestone = _dal.Task.Create(milestone);

            foreach (var taskListwithDeps in groupDependencies)
            {
                var t = taskListwithDeps._value.ToList();
                if (t.SequenceEqual(groupOfDepentOnTasks))
                    newDepsList.Add(new DO.Dependency(-1, taskListwithDeps._key!.Value, idMilestone));
            }

            foreach (var dep in groupOfDepentOnTasks)
            {
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

    private DateTime? updateDeadlines(int taskId, int endMilestoneId, List<DO.Dependency> depList)
    {
        if (taskId == endMilestoneId)
            return _dal.EndProjectDate;
        DO.Task currentTask = _dal.Task.Read(taskId) ?? throw new BO.BlNullPropertyException($"Task with Id {taskId} does not exists");

        List<int?> listOfDepentOnCurrentTask = (from dep in depList
                                                where dep.DependsOnTask == taskId
                                                select dep.DependentTask).ToList();

        DateTime? deadline = null;
        foreach (int task in listOfDepentOnCurrentTask)
        {
            DO.Task readTask = _dal.Task.Read(taskId)!;
            if (readTask.Deadline is null)
                readTask = readTask with { Deadline = updateDeadlines(task, endMilestoneId, depList) };
            if (deadline is null || readTask.Deadline - readTask.RequiredEffortTime < deadline)
                deadline = readTask.Deadline - readTask.RequiredEffortTime;
        }
        if (deadline > _dal.EndProjectDate)
            throw new BO.BlInsufficientTime("There is insufficient time to complete this task\n");
        currentTask = currentTask with { Deadline = deadline };

        _dal.Task.Update(currentTask);
        return currentTask.Deadline;
    }

    private DateTime? updateScheduledDates(int taskId, int startMilestoneId, List<DO.Dependency> depList)
    {
        if (taskId == startMilestoneId)
            return _dal.StartProjectDate;
        DO.Task currentTask = _dal.Task.Read(taskId) ?? throw new BO.BlNullPropertyException($"Task with Id {taskId} does not exists");

        List<int?> listOfTasksThatCurrentDepsOnThem = (from dep in depList
                                                       where dep.DependentTask == taskId
                                                       select dep.DependsOnTask).ToList();

        DateTime? scheduledDate = null;
        foreach (int? task in listOfTasksThatCurrentDepsOnThem)
        {
            DO.Task readTask = _dal.Task.Read(taskId)!;
            if (readTask.ScheduledDate is null)
                readTask = readTask with { ScheduledDate = updateDeadlines((int)task!, startMilestoneId, depList) };
            if (scheduledDate is null || readTask.ScheduledDate + readTask.RequiredEffortTime > scheduledDate)
                scheduledDate = readTask.ScheduledDate + readTask.RequiredEffortTime;
        }

        if (scheduledDate < _dal.StartProjectDate)
            throw new BO.BlInsufficientTime("There is insufficient time to complete this task\n");
        currentTask = currentTask with { ScheduledDate = scheduledDate };


        _dal.Task.Update(currentTask);
        return currentTask.ScheduledDate;
    }
    #endregion

    public void CreateProjectsSchedule()
    {
        //DateTime time;
        //Console.WriteLine("enter date of start: \n");
        //bool successStart = DateTime.TryParse(Console.ReadLine()!,out time);
        //if (!successStart)
        //    throw new BlInvalidInput("Not valid DateTime Input.\n");
        //else
        //    _dal.StartProjectDate = time;

        //Console.WriteLine("enter date of end: \n");
        //bool successEnd = DateTime.TryParse(Console.ReadLine()!, out time);
        //if (!successEnd)
        //    throw new BlInvalidInput("Not valid DateTime Input.\n");
        //else
        //    _dal.EndProjectDate = time;

        List<DO.Dependency?> dependencies = _dal.Dependency.ReadAll().ToList();
        List<DO.Dependency> newDepsList = createMilestones(dependencies);
        _dal.Dependency.Reset();
        foreach (var dep in newDepsList)
        {
            _dal.Dependency.Create(dep);
        }


        List<DO.Task?> allTasks = _dal.Task.ReadAll().ToList();
        int startMilestoneId = allTasks.Where(task => task!.Alias == "start").Select(task => task!.Id).First();

        DO.Task startMilestone = _dal.Task.Read(startMilestoneId)!;
        if (startMilestone is not null)
            startMilestone = startMilestone with { ScheduledDate = _dal.StartProjectDate };

        int endMilestoneId = allTasks.Where(task => task!.Alias == "end").Select(task => task!.Id).First();

        DO.Task endMilestone = _dal.Task.Read(endMilestoneId)!;
        if (endMilestone is not null)
            endMilestone = endMilestone with { Deadline = _dal.EndProjectDate };

        startMilestone = startMilestone! with { Deadline = updateDeadlines(startMilestoneId, endMilestoneId, newDepsList) };
        _dal.Task.Update(startMilestone!);

        endMilestone = endMilestone! with { ScheduledDate = updateScheduledDates(endMilestoneId, startMilestoneId, newDepsList) };
        _dal.Task.Update(endMilestone);
    }
        
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
            Status = (Status)(milestoneFromDo.ScheduledDate is null ? 0 :
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

    public void Update(BO.Milestone m)
    {
        DO.Task doMilestone = new DO.Task(m.Id, m.Description, m.Alias, true, m.CreatedAtDate, new TimeSpan(0), null, m.ForecastDate, m.DeadlineDate, m.CompleteDate, null, m.Remarks, null, null);
        try
        {
            _dal.Task.Update(doMilestone);
        }
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"An object of type Task with ID {doMilestone.Id} does not exist", exception);
        }
    }

    public void setDates(DateTime start, DateTime end)
    {
        _dal.StartProjectDate= start;
        _dal.EndProjectDate= end;
    }
}
