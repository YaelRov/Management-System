
using DalApi;
using System.Diagnostics;

namespace Dal;
//הי, יוני. מה שלומך? דש חם ממני
sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public DateTime StartProjectDate { get; set; }
    public DateTime EndProjectDate { get; set;}

    public void Reset()
    {
        XMLTools.ResetFile("engineers");
        XMLTools.ResetFile("tasks");
        XMLTools.ResetFile("dependencies");
    }
}
