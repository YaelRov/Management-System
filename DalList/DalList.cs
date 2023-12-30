
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

    DateTime? IDal.StartProjectDate {
        get => DataSource.Config.startProjectDate;
        set => DataSource.Config.startProjectDate = value;
    }
    DateTime? IDal.EndProjectDate {
        get => DataSource.Config.endProjectDate;
        set => DataSource.Config.endProjectDate = value;
    }

    public void Reset()
    {
        DataSource.Engineers.Clear();
        DataSource.Tasks.Clear();
        DataSource.Dependencies.Clear();
    }
}

