

namespace BlApi;
/// <summary>
/// interface for bl entities
/// </summary>
public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public IMilestone Milestone { get; }
}

