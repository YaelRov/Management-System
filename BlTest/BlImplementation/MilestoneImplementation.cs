
namespace BlTest.BlImplementation;
using BlApi;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void CreateProjectsSchedule()
    {
        throw new NotImplementedException();
    }

    public BO.Milestone? Read(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Milestone item)
    {
        throw new NotImplementedException();
    }
}
