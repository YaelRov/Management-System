
namespace BlImplementation;
using BlApi;
using BO;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void CreateProjectsSchedule()
    {

    }

    public BO.Milestone? Read(int id)
    {
        DO.Task milestoneFromDo = _dal.Task.Read(id) ?? throw new BlDoesNotExistException($"An object of type Milestone with ID {id} does not exist");
        BO.Milestone milestone = new()
        {
            Id = milestoneFromDo.Id,
            Description= milestoneFromDo.Description,
            Alias= milestoneFromDo.Alias,
            Status = Status.InJeopardy,//to fix
            CreatedAtDate = milestoneFromDo.CreatedAt,
            ForecastDate= milestoneFromDo.ForecastDate, 
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
