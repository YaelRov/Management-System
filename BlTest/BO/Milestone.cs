
namespace BlTest.BO;

internal class Milestone
{
    public int Id { get; init; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public BO.Status Status { get; set; }
    public DateTime CreatedAtDate { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public double? CompletionPercentage { get; set; }
    public string? Remarks { get; set; }

    public List<BO.TaskInList>? Dependencies {  get; set; }


    public override string ToString() => this.ToStringProperty();
    string ToStringProperty()
    {
        string str = $" Id: {Id}, Description: {Description}, Alias: {Alias}, Status:{Status}, Created At Date: {CreatedAtDate}";
        str += ForecastDate is not null ? $", Forecast Date: {ForecastDate}" : "";
        str += DeadlineDate is not null ? $", Deadline Date: {DeadlineDate}" : "";
        str += CompleteDate is not null ? $", Complete Date: {CompleteDate}" : "";
        str += CompletionPercentage is not null ? $", Completion Percentage: {CompletionPercentage}" : "";
        str += Remarks is not null ? $", Remarks: {Remarks}" : "";
        str += Dependencies is not null ? $", Dependencies: {Dependencies}" : "";

        return str;
    }
}
