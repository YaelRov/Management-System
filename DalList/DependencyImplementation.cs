
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextDependId;
        Dependency newDependency = new Dependency(newId,item.DependentTask, item.DependsOnTask);
        DataSource.Dependencies.Add(newDependency);
        Dependency.counterDependencies++;
        return newId;
    }

    public void Delete(int id)
    {
        Dependency? obj = DataSource.Dependencies.Find(curDep => curDep.Id == id);
        if (obj is null)
            throw new Exception($"An object of type Dependency with ID {id} does not exist");
        DataSource.Dependencies.Remove(obj);
        Dependency.counterDependencies--;

    }

    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.Find(curTask => curTask.Id == id);
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency item)
    {
        Dependency? obj = DataSource.Dependencies.Find(curDep => curDep.Id == item.Id);
        if (obj == null)
            throw new Exception($"An object of type Dependency with ID {item.Id} does not exist");
        DataSource.Dependencies.Remove(obj);
        DataSource.Dependencies.Add(item);
    }
}
