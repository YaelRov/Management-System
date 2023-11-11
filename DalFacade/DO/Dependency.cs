
namespace DO;
/// <summary>
/// An entity which describes an Dependency
/// </summary>
/// <param name="id">primary key, auto id number </param>
/// <param name="dependentTask">NN</param>
/// <param name="dependsOnTask">NN</param>
public record Dependency
(
    int id,
    int? DependentTask,//nullable
    int? DependsOnTask//nullable
)
{
    public static int counterDependencies = 0;//counter for how many dependencies were created
    public int Id { get { return id; } }//Id cannot be change so there is no set function
    Dependency() :this(0,null,null) { }//c-tor
}
