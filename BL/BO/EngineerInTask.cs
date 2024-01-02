

namespace BO;
/// <summary>
/// class for details about engineer in task
/// </summary>
public class EngineerInTask
{
    public int Id { get; init; }
    public string Name { get; set; }
    public override string ToString() => this.ToStringProperty();

}
