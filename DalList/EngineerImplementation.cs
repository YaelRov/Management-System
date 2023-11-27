
namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// gets an engineer object and add it to the list
    /// </summary>
    /// <param name="item">object type Engineer</param>
    /// <returns>id of the engineer, type int</returns>
    /// <exception cref="DalAlreadyExistsException"></exception>
    public int Create(Engineer item)
    {
        if (Read(item.Id) is not null)//if exist already the same id number
            throw new DalAlreadyExistsException($"An object of type Engineer with ID {item.Id} already exists");
        DataSource.Engineers.Add(item);//add to the list
        Engineer.counterEngineers++;//add 1 to the counter of the engineers
        return item.Id;
    }
    /// <summary>
    /// gets an id number of an engineer and delete it out from the list
    /// </summary>
    /// <param name="id">int</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        Engineer? foundEngineer = Read(id);
        if (foundEngineer is null)//if the object does not exist
            throw new DalDoesNotExistException($"An object of type Engineer with ID {id} does not exist");
        DataSource.Engineers.Remove(foundEngineer);//remove from the list
        Engineer.counterEngineers--;//subtract 1 from the counter
    }
    /// <summary>
    /// get id of a engineer to read
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>an object type Engineer</returns>
    public Engineer? Read(int id)
    {
        //find the engineer in the list
        var foundEngineer = DataSource.Engineers
                     .Where(curEngineer => curEngineer.Id == id)
                     .FirstOrDefault();
        return foundEngineer;
    }

    /// <summary>
    /// get condition of a engineer to read
    /// </summary>
    /// <param name="filter">a condition function</param>
    /// <returns>the first element that satisfies the conditin</returns>
    public Engineer? Read(Func<Engineer, bool> filter) //stage 2
    {
        return DataSource.Engineers
              .FirstOrDefault(filter);
    }
    /// <summary>
    /// reading all the list of the engineers
    /// </summary>
    /// <returns>IEnumerable of type Engineer?</returns>

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else
            return DataSource.Engineers.Where(filter);
    }

    /// <summary>
    /// updating an engineer
    /// </summary>
    /// <param name="item">an object type Engineer</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Engineer item)
    {
        //find the object in the list
        var foundEngineer = Read(item.Id);
        if (foundEngineer is null)//if does not exist in the list
            throw new DalDoesNotExistException($"An object of type Engineer with ID {item.Id} does not exist");
        DataSource.Engineers.Remove(foundEngineer);//delete the old engineer
        DataSource.Engineers.Add(item);//add the updated one
    }
}
