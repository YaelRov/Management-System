
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// gets a dependency object and add it to the list
    /// </summary>
    /// <param name="item">object type Dependency</param>
    /// <returns>id of the dependency, type int</returns>
    /// <exception cref="Exception"></exception>
    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextDependId;//get a barcode from the config class
        Dependency newDependency = new Dependency(newId,item.DependentTask, item.DependsOnTask);//create a new dependency object
        DataSource.Dependencies.Add(newDependency);//add the dependency object to the list
        Dependency.counterDependencies++;//add 1 to the counter of the dependencies
        return newId;
    }
    /// <summary>
    /// gets an id number of a dependency and delete it out from the list
    /// </summary>
    /// <param name="id">int</param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        Dependency? obj = DataSource.Dependencies.Find(curDep => curDep.Id == id);//checks if it exsists in the list
        if (obj is null)//if the object does not exist
            throw new Exception($"An object of type Dependency with ID {id} does not exist");
        DataSource.Dependencies.Remove(obj);//remove from the list
        Dependency.counterDependencies--;//subtract 1 from the counter

    }
    /// <summary>
    /// get id of a dependency to read
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>an object type Dependency</returns>
    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.Find(curTask => curTask.Id == id);//find the dependency in the list
    }
    /// <summary>
    /// reading all the list of the dependencies
    /// </summary>
    /// <returns>copy of the dependencies list</returns>
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }
    /// <summary>
    /// updating a dependency
    /// </summary>
    /// <param name="item">an object type Dependency</param>
    /// <exception cref="Exception"></exception>
    public void Update(Dependency item)
    {
        Dependency? obj = DataSource.Dependencies.Find(curDep => curDep.Id == item.Id);//find the object in the list
        if (obj == null)//if does not exist in the list
            throw new Exception($"An object of type Dependency with ID {item.Id} does not exist");
        DataSource.Dependencies.Remove(obj);//delete the old dependency
        DataSource.Dependencies.Add(item);//add the updated one
    }
}
