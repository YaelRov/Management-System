
namespace DO;

/// <summary>
/// An entity which describes an engineer
/// </summary>
/// <param name="id">primary key NN</param>
/// <param name="name">NN</param>
/// <param name="email">NN</param>
/// <param name="level">engineer's experience NN</param>
/// <param name="cost">payment per hour NN</param>

public record Engineer
(
    int id,
    string Name,
    string Email,
    EngineerExperience Level,
    double Cost
)
{
    public static int counterEngineers = 0;//counter for how many engineers were created
    public int Id { get { return id; } }//Id cannot be change so there is no set function
    Engineer() : this(-1,"","", EngineerExperience.Novice,0) { }//c-tor
}

