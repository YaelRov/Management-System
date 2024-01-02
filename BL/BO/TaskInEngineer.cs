
namespace BO;
/// <summary>
/// class for details about task in engineer
/// </summary>
public class TaskInEngineer
{
    public int Id { get; init; }
    public string Alias { get; set; }

    public override string ToString() => this.ToStringProperty();
}
