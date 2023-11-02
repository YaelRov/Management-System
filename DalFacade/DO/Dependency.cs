
namespace DO;
/// <summary>
/// An entity which describes an Dependency
/// </summary>
/// <param name="id">primary key, auto id number </param>
/// <param name="dependentTask">NN</param>
/// <param name="dependsOnTask">NN</param>
public record Dependency
{
    #region Config
    internal static class Config
    {
        internal const int startDependId = 10000;
        private static int nextDependId = startDependId;
        internal static int NextDependId { get => nextDependId++; }
    }
    #endregion
    #region characters
    int id;
    int dependentTask;
    int dependsOnTask
    #endregion
    #region properties
    public int Id { get { return id; } }
    public int DependentTask {  get { return dependentTask; } set { dependentTask = value; } }
    public int DependsOnTask { get { return dependsOnTask; } set { dependsOnTask = value; } }
    #endregion
    #region constructors
    public Task(int dependentTask, int dependsOnTask)
    {
        this.id = Config.NextDependId;
        this.dependentTask = dependentTask;   
        this.dependsOnTask = dependsOnTask;
    }
    public Task() { }
    #endregion
}
