
namespace BlImplementation;
using BlApi;
using BO;
using System.Runtime.Intrinsics.Arm;
using System.Xml.Linq;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private List<DO.Dependency> createMilestones( List<DO.Dependency> dependencies)
    {
        var groupDependencies = (from dep in dependencies
                                where dep.DependentTask is not null && dep.DependsOnTask is not null
                                group dep by dep.DependentTask into gropByDependentTask
                                let depList=(from dep in gropByDependentTask
                                            select dep.DependsOnTask).Order()
                                select new {_key= gropByDependentTask.Key, _value=depList});
        var listAfterDistinct = (from dep in groupDependencies
                                 select dep._value).Distinct();

        List<DO.Dependency> newDepsList = new List<DO.Dependency>();
        int i = 1;
        //List<DO.Task?> tasks = _dal.Task.ReadAll().ToList();
        foreach (var groupOfDepentOnTasks in listAfterDistinct)
        {
            DO.Task milestone = new DO.Task(-1, "Description",$"M{i}", true, DateTime.Now, null, null, null, null, null, null, null, null, null);
            int idMilestone = _dal.Task.Create(milestone);

            foreach (var taskListwithDeps in groupDependencies)
            {
                if (taskListwithDeps._value == groupOfDepentOnTasks)
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

        List<DO.Task?> oldTasks = _dal.Task.ReadAll().ToList();
        //find tasks that depent on start
        var notDepTasks = (from task in oldTasks
                          where !(from taskDep in groupDependencies
                                  select taskDep._key).Any(t => t==task.Id)
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

    public void CreateProjectsSchedule(List<DO.Task> tasks, List<DO.Dependency> dependencies)
    {
        List<DO.Dependency> newDepsList = createMilestones(dependencies);
        _dal.Dependency.Reset();
        foreach (var dep in newDepsList)
        {
            _dal.Dependency.Create(dep);
        }
    }

    public BO.Milestone? Read(int id)
    {
        DO.Task milestoneFromDo = _dal.Task.Read(id) ?? throw new BlDoesNotExistException($"An object of type Milestone with ID {id} does not exist");
        DateTime? forecastDate =  null;
        if(milestoneFromDo.Start is not null&& milestoneFromDo.RequiredEffortTime is not null)
        {
            TimeSpan ts = milestoneFromDo.RequiredEffortTime?? new TimeSpan(0,0,0);
            forecastDate = milestoneFromDo.Start?.Add(ts);
        }

        BO.Milestone milestone = new()
        {
            Id = milestoneFromDo.Id,
            Description= milestoneFromDo.Description,
            Alias= milestoneFromDo.Alias,
            Status = Status.InJeopardy,//to fix
            CreatedAtDate = milestoneFromDo.CreatedAt,
            ForecastDate= forecastDate, 
            DeadlineDate= milestoneFromDo.Deadline,
            CompleteDate= milestoneFromDo.Complete,
            CompletionPercentage = 0,//to fix
            Remarks = milestoneFromDo.Remarks
        };
        return milestone;
    }

    public void Update(BO.Milestone item)
    {
        
    }
}
