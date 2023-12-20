
//using System.Data.Common;

namespace DO;
/// <summary>
/// An entity which describes a Task
/// </summary>
/// <param name="id">primary key, auto id number </param>
/// <param name="description">NN</param>
/// <param name="alias">NN</param>
/// <param name="milestone">engineer's experience NN</param>
/// <param name="createdAt">payment per hour NN</param>
/// <param name="Start">nullable</param>
/// <param name="ScheduledDate">nullable</param>
/// <param name="ForecastDate">nullable</param>
/// <param name="Deadline">nullable</param>
/// <param name="Complete">nullable</param>
/// <param name="Deliverables">nullable</param>
/// <param name="Remarks">nullable</param>
/// <param name="EngineerId">nullable</param>
/// <param name="ComplexityLevel">enum type, nullable</param>

public record Task
(
int id,
string Description,
string Alias,
bool Milestone,
DateTime CreatedAt,
TimeSpan? RequiredEffortTime,
DateTime? Start,
DateTime? ScheduledDate,
DateTime? ForecastDate,
DateTime? Deadline,
DateTime? Complete,
string? Deliverables,
string? Remarks,
int? EngineerId,
EngineerExperience? ComplexityLevel
)
{
    public int Id { get { return id; } }//Id cannot be change so there is no set function

    public static int counterTasks = 0;//counter for how many tasks were created
    Task() : this(-1,"","", false,DateTime.Now,null,null, null, null, null,null,null,null,null,null) { }//c-tor
}

