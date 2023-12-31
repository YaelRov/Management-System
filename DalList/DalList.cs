
namespace Dal;
using DalApi;
using System;

sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public DateTime StartProjectDate { get ; set; }
    public DateTime EndProjectDate { get ; set;  }
    
    public void Reset()
    {
        DataSource.Engineers.Clear();
        DataSource.Tasks.Clear();
        DataSource.Dependencies.Clear();
    }
}

