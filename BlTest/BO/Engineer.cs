

using DO;

namespace BlTest.BO;

public class Engineer
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public EngineerExperience Level { get; set; }
    public double Cost { get; set; }
    public BO.TaskInEngineer? Task { get; set; }    

    public override string ToString() => this.ToStringProperty();
    string ToStringProperty()
    {
        string str = $"Id: {Id}, Name:{Name}, Email:{Email}, Level:{Level}, Cost:{Cost}";
        str += Task is not null ? $", Task: {Task}" : "";
        return str;
    }
}
