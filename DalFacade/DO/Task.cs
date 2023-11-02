

namespace DO;

public record Task
{
    #region characters
    int id;
    string description;
    string alias;
    bool milestone;
    DateTime createdAt;
    DateTime? start;
    DateTime? scheduledDate;
    DateTime? forecastDate;
    DateTime? deadline;
    DateTime? complete;
    string? deliverables;
    string? remarks;
    int? engineerId;
    EngineerExperience? complexityLevel;
    #endregion
    #region properties
    public int ID { get { return id; }}
    public string Description { get { return description; } set {description = value; } }
    public string Alias { get { return alias; } set { alias = value; } }
    public bool Milestone { get {  return milestone; } set {  milestone = value; } }
    public DateTime CreatedAt { get {  return createdAt; } set {  createdAt = value; } }
    public DateTime? Start { get { return start; } set { start = value; } }
    public DateTime? ScheduledDate { get {  return scheduledDate; } set {  scheduledDate = value; } }
    public DateTime? ForecastDate { get { return forecastDate; } set {  forecastDate = value; } }
    public DateTime? Deadline { get { return deadline; } set {  deadline = value; } }
    public DateTime? Complete { get { return complete; } set {  complete = value; } }
    public string Deliverables { get { return deliverables; } set { deliverables = value; } }
    public string Remarks { get { return remarks; } set { remarks = value; } }
    public int? EngineerId { get {  return engineerId; } set { engineerId = value; } }
    public EngineerExperience? ComplexityLevel { get { return complexityLevel; } set { complexityLevel = value; } }

    #endregion
    #region constructors

    #endregion
}
