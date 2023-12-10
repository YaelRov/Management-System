

using DO;

namespace BlTest.BO;

public class Task
{

    public int Id { get; init; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public bool Milestone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime? Complete { get; set; }
    public string? Deliverables { get; set; }
    public string? Remarks { get; set; }
    public int? EngineerId { get; set; }
    public EngineerExperience? ComplexityLevel { get; set; }
    public override string ToString() => this.ToStringProperty();
    string ToStringProperty()
    {
        string str = $" Id: {Id}, Description: {Description}, Alias: {Alias}, Milestone: {Milestone}, CreatedAt: {CreatedAt}";
        str +=  Start is not null ?  $", Start: {Start}" : "";
        str += ScheduledDate is not null ?  $", Scheduled Date: {ScheduledDate}" : "";
        str += ForecastDate is not null ?  $", Forecast Date: {ForecastDate}" : "";
        str += Deadline is not null ?  $", Deadline: {Deadline}" : "";
        str += Complete is not null ?  $", Complete: {Complete}" : "";
        str += Deliverables is not null ?  $", Deliverables: {Deliverables}" : "";
        str += Remarks is not null ?  $", Remarks: {Remarks}" : "";
        str += EngineerId is not null ?  $", Engineer Id: {EngineerId}" : "";
        str += ComplexityLevel is not null ?  $", Complexity Level: {ComplexityLevel}" : "";
        return str;
    }
}

