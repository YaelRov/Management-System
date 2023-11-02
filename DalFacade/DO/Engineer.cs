
namespace DO;

/// <summary>
/// An entity which describes an engineer
/// Every engineer has :
/// int id number that is the primery key of the entity NN
/// string of name NN
/// string of email NN
/// engineer's level (EngineerExperience) NN
/// doble cost per hour NN 
/// </summary>
/// <param name="id">primery key NN</param>
/// <param name="name">NN</param>
/// <param name="email">NN</param>
/// <param name="level">engineer's experience NN</param>
/// <param name="cost">payment per hour NN</param>

public record Engineer
{
    #region characters
    int id;
    string name;
    string email;
    EngineerExperience level;
    double cost;
    #endregion
    #region properties
    public int ID { get { return id; } }
    public string Name { get { return name; } set { name = value; } }
    public string Email { get { return email; } set { email = value; } }
    public EngineerExperience Level { get { return level; } set { level = value; } }
    public double Cost { get { return cost; } set { cost = value; } }
    #endregion
    #region constructor
    public Engineer(int id, string name = "", string email = "", EngineerExperience level = EngineerExperience.Rookie, double cost = 28.12)
    {
        this.id = id;
        this.name = name;
        this.email = email;
        this.level = level;
        this.cost = cost;
    }
    public Engineer(){}
    #endregion
}
