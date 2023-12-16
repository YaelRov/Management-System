
using BlTest.BlApi;
using BlTest.BO;

namespace BlTest.BlImplementation;

internal class Bl : IBl
{
    public IEngineer Engineer =>  new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IMilestone Milestone =>  new MilestoneImplementation();

}
