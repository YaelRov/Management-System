

namespace BO;
/// <summary>
/// Dependency Class
/// </summary>
public class Dependency
{
    public int Id { get; init; }
    public int? DependentTask { get; set; }
    public int? DependsOnTask { get; set; }
    public override string ToString() => this.ToStringProperty();

}
