

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
    //public override string ToString() => this.ToStringProperty();
}

