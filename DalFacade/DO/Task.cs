
using System.Data.Common;

namespace DO;
/// <summary>
/// An entity which describes a Task
/// </summary>
/// <param name="id">primary key, auto id number </param>
/// <param name="description">NN</param>
/// <param name="alias">NN</param>
/// <param name="milestone">engineer's experience NN</param>
/// <param name="createdAt">payment per hour NN</param>

public record Task
{
    #region characters
    readonly int id;
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
    public int Id { get { return id; }}
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

    public Task(string description = "", string alias = "", bool milestone = false, DateTime createdAt =  new DateTime(), DateTime start = new DateTime(), DateTime scheduledDate = new DateTime(), DateTime forecastDate = new DateTime(), DateTime deadline = new DateTime(), DateTime complete = new DateTime(), string deliverables="", string remarks="", int engineerId = -1, EngineerExperience? complexityLevel=0)
    {
        this.id = -1;
        this.description = description;
        this.alias = alias;
        this.milestone = milestone; 
        this.createdAt = createdAt; 
        this.scheduledDate = scheduledDate;
        this.forecastDate = forecastDate;
        this.deadline = deadline;
        this.complete = complete;
        this.deliverables = deliverables;
        this.remarks = remarks;
        this.engineerId = engineerId;
        this.complexityLevel = complexityLevel;
    }
    public Task(){ }
    #endregion
}
