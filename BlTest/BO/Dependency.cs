

namespace BlTest.BO;
/// <summary>
/// Dependency Class
/// </summary>
public class Dependency
{
    public int Id { get; init; }
    public int? DependentTask { get; set; }
    public int? DependsOnTask { get; set; } 
    public override string ToString() => this.ToStringProperty();

    string ToStringProperty()
    {
        string str= $"Id: {Id}";
        str += DependentTask is not null ? $", Dependent Task: {DependentTask}" : "";
        str += DependsOnTask is not null ? $", Depends On Task: {DependsOnTask}" : "";
        return str;
    }
}
