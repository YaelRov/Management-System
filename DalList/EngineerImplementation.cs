
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
    /// <exception cref="Exception"></exception>
    public int Create(Engineer item)
    {
        Engineer? obj = DataSource.Engineers.Find(curEngineer => curEngineer.Id == item.Id);//check if the same id is exist already
        if (obj is not null)//if exist already the same id number
            throw new Exception($"An object of type Engineer with ID {item.Id} already exists");
        DataSource.Engineers.Add(item);//add to the list
        Engineer.counterEngineers++;//add 1 to the counter of the engineers
        return item.Id;
    }
    /// <summary>
    /// gets an id number of an engineer and delete it out from the list
    /// </summary>
    /// <param name="id">int</param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        var foundEngineer = DataSource.Engineers
                      .Where(curEngineer => curEngineer.Id == id)
                      .FirstOrDefault();
        if (foundEngineer is null)//if the object does not exist
            throw new Exception($"An object of type Engineer with ID {id} does not exist");
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
    /// reading all the list of the engineers
    /// </summary>
    /// <returns>copy of the engineers list</returns>
    public List<Engineer> ReadAll()
    {
        var returnedList = DataSource.Engineers
                    .Where(curEngineer => true)
                    .ToList<Engineer>();
        return returnedList;
    }
    /// <summary>
    /// updating an engineer
    /// </summary>
    /// <param name="item">an object type Engineer</param>
    /// <exception cref="Exception"></exception>
    public void Update(Engineer item)
    {
        //find the object in the list
        var foundEngineer = DataSource.Engineers
             .Where(curEngineer => curEngineer.Id == item.Id)
             .FirstOrDefault();
        if (foundEngineer is null)//if does not exist in the list
            throw new Exception($"An object of type Engineer with ID {item.Id} does not exist");
        DataSource.Engineers.Remove(foundEngineer);//delete the old engineer
        DataSource.Engineers.Add(item);//add the updated one
    }
}
