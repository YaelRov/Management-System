
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
    public static int counterEngineers = 0;
    public int Id { get { return id; } }
    Engineer() : this(-1,"","", EngineerExperience.Novice,0) { }
}


/*
     #region characters
    int id;
    string name;
    string email;
    EngineerExperience level;
    double cost;

    public static int counterEngineers = 0;

    #endregion
    #region properties
    public int Id { get { return id; } }
    public string Name { get { return name; } set { name = value; } }
    public string Email { get { return email; } set { email = value; } }
    public EngineerExperience Level { get { return level; } set { level = value; } }
    public double Cost { get { return cost; } set { cost = value; } }
    #endregion
    #region constructors
    public Engineer(int id, string name = "", string email = "", EngineerExperience level = EngineerExperience.Novice, double cost = 28.12)
    {
        this.id = id;
        this.name = name;
        this.email = email;
        this.level = level;
        this.cost = cost;
    }
    public Engineer(){}
    #endregion
 */