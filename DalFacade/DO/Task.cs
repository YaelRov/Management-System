
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
(
int id,
string Description,
string Alias,
bool Milestone,
DateTime CreatedAt,
DateTime? Start,//nullable
DateTime? ScheduledDate,//nullable
DateTime? ForecastDate,//nullable
DateTime? Deadline,//nullable
DateTime? Complete,//nullable
string? Deliverables,//nullable
string? Remarks,//nullable
int? EngineerId,//nullable
EngineerExperience? ComplexityLevel//enum type, nullable
)
{
    public int Id { get { return id; } }//Id cannot be change so there is no set function

    public static int counterTasks = 0;//counter for how many tasks were created
    Task() : this(-1,"","", false,DateTime.Now,null, null, null, null,null,null,null,null,null) { }//c-tor
}
