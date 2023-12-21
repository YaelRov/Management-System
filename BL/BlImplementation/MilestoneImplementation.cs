
namespace BlImplementation;
using BlApi;
using BO;
using System.Runtime.Intrinsics.Arm;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void CreateProjectsSchedule(List<DO.Task> tasks, List<DO.Dependency> dependencies)
    {
        var groupDependencies = (from dep in dependencies
                                where dep.DependentTask is not null && dep.DependsOnTask is not null
                                group dep by dep.DependentTask into gropByDependentTask
                                let depList=(from dep in gropByDependentTask
                                            select dep.DependsOnTask).Order()
                                select new {_key= gropByDependentTask.Key, _value=depList});
        var listAfterDistinct = (from dep in groupDependencies
                                 select dep._value).Distinct();

        List<DO.Dependency> newTasksList = new List<DO.Dependency>();
        int i = 1;
        foreach (var item in listAfterDistinct)
        {
            DO.Task milestone = new DO.Task(-1, "Description",$"M{i}", true, DateTime.Now, null, null, null, null, null, null, null, null, null);
            i++;
            tasks.Add(milestone);

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
