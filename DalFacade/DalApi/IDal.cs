
namespace DalApi;

public interface IDal
{
    IEngineer Engineer { get; }
    ITask Task { get; }
    IDependency Dependency { get; }
    DateTime StartProjectDate { get; set; }
    DateTime EndProjectDate { get; set; }

}
