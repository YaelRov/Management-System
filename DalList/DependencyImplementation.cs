
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)//gets a dependency object and add it to the list
    {
        int newId = DataSource.Config.NextDependId;//get a barcode from the config class
        Dependency newDependency = new Dependency(newId,item.DependentTask, item.DependsOnTask);//create a new dependency object
        DataSource.Dependencies.Add(newDependency);//add the dependency object to the list
        Dependency.counterDependencies++;//add 1 to the counter of the dependencies
        return newId;//return the id of the dependency
    }

    public void Delete(int id)// gets an id number of a dependency and delete it out from the list
    {
        Dependency? obj = DataSource.Dependencies.Find(curDep => curDep.Id == id);//find the dependency with this id number
        if (obj == null)//if it's not exsist in the list
            throw new Exception($"An object of type Dependency with ID {id} does not exist");
        DataSource.Dependencies.Remove(obj);//remove from the list
        Dependency.counterDependencies--;//subtract 1 from the counter

    }

    public Dependency? Read(int id)//get id of a dependency to read
    {
        return DataSource.Dependencies.Find(curTask => curTask.Id == id);//find the dependency in the list
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);//
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
