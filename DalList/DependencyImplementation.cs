
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
        Dependency? foundDep = Read(id);
        if (foundDep is null)//if the object does not exist
            throw new DalDoesNotExistException($"An object of type Dependency with ID {id} does not exist");
        DataSource.Dependencies.Remove(foundDep);//remove from the list
        Dependency.counterDependencies--;//subtract 1 from the counter

    }
    /// <summary>
    /// get id of a dependency to read
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>an object type Dependency</returns>
    public Dependency? Read(int id)
    {
        //find the dependency in the list
        var foundDep = DataSource.Dependencies
                      .Where(curDep => curDep.Id == id)
                      .FirstOrDefault();
        return foundDep;
    }
    public Dependency? Read(Func<Dependency, bool> filter) //stage 2
    {
        return DataSource.Dependencies
              .FirstOrDefault(filter);
    }
    /// <summary>
    /// reading all the list of the dependencies
    /// </summary>
    /// <returns>copy of the dependencies list</returns>

    public IEnumerable<Dependency?> ReadAll(Func<Dependency?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Dependencies.Select(item => item);
        else
            return DataSource.Dependencies.Where(filter);
    }
    /// <summary>
    /// updating a dependency
    /// </summary>
    /// <param name="item">an object type Dependency</param>
    /// <exception cref="Exception"></exception>
    public void Update(Dependency item)
    {
        //find the object in the list
        Dependency? foundDep = Read(item.Id);
        if (foundDep is null)//if does not exist in the list
            throw new DalDoesNotExistException($"An object of type Dependency with ID {item.Id} does not exist");
        DataSource.Dependencies.Remove(foundDep);//delete the old dependency
        DataSource.Dependencies.Add(item);//add the updated one
    }
}
