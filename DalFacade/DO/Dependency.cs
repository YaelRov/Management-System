
namespace DO;
/// <summary>
/// An entity which describes an Dependency
/// </summary>
/// <param name="id">primary key, auto id number </param>
/// <param name="dependentTask">NN</param>
/// <param name="dependsOnTask">NN</param>
public record Dependency
{
    #region characters
    readonly int id;
    int dependentTask;
    int dependsOnTask;
    #endregion
    #region properties
    public int Id { get { return id; } }
    public int DependentTask {  get { return dependentTask; } set { dependentTask = value; } }
    public int DependsOnTask { get { return dependsOnTask; } set { dependsOnTask = value; } }
    #endregion
    #region constructors
    public Dependency(int dependentTask, int dependsOnTask, int id = -1)
    {
        this.id = id;
        this.dependentTask = dependentTask;   
        this.dependsOnTask = dependsOnTask;
    }
    public Dependency() { }
    #endregion
}
