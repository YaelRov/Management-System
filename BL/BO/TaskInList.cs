
using DO;
using System.Numerics;
namespace BO;
/// <summary>
/// class for details about task in list
/// </summary>
public class TaskInList
{
    public int Id { get; init; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public Status? Status { get; set; }

    public override string ToString() => this.ToStringProperty();
}
