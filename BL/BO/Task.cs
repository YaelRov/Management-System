

using DO;

namespace BO;

public class Task
{
    public int Id { get; init; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public DateTime CreatedAt { get; set; }
    public BO.Status? Status { get; set; }
    public BO.MilestoneInTask? Milestone { get; set; }
    public DateTime? BaselineStartDate { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime? Complete { get; set; }
    public string? Deliverables { get; set; }
    public string? Remarks { get; set; }
    public BO.EngineerInTask? Engineer { get; set; }
    public EngineerExperience? ComplexityLevel { get; set; }
    public override string ToString() => this.ToStringProperty();
    string ToStringProperty()
    {
        string str = $" Id: {Id}, Description: {Description}, Alias: {Alias}, Milestone: {Milestone}, CreatedAt: {CreatedAt}";
        str += Status is not null ? $", Status: {Status}" : "";
        str += Milestone is not null ? $", Milestone: {Milestone}" : "";
        str += BaselineStartDate is not null ? $", Baseline Start Date: {BaselineStartDate}" : "";
        str += Start is not null ? $", Start: {Start}" : "";
        str += ScheduledDate is not null ? $", Scheduled Date: {ScheduledDate}" : "";
        str += ForecastDate is not null ? $", Forecast Date: {ForecastDate}" : "";
        str += Deadline is not null ? $", Deadline: {Deadline}" : "";
        str += Complete is not null ? $", Complete: {Complete}" : "";
        str += Deliverables is not null ? $", Deliverables: {Deliverables}" : "";
        str += Remarks is not null ? $", Remarks: {Remarks}" : "";
        str += Engineer is not null ? $", Engineer: {Engineer}" : "";
        str += ComplexityLevel is not null ? $", Complexity Level: {ComplexityLevel}" : "";
        return str;
    }
}

