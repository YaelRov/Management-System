
namespace Dal;

using DalApi;
using DO;

using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)//gets an engineer object and add it to the list
    {
        Engineer? obj = DataSource.Engineers.Find(curEngineer => curEngineer.Id == item.Id);//check if the same id is exist already
        if (obj is not null)//if exist already the same id number
            throw new Exception($"An object of type Engineer with ID {item.Id} already exists");
        DataSource.Engineers.Add(item);//add to the list
        Engineer.counterEngineers++;//add 1 to the counter of the engineers
        return item.Id;//return the id of the engineer
    }

public void Delete(int id)// gets an id number of an engineer and delete it out from the list
    {
        Engineer? obj = DataSource.Engineers.Find(curEngineer => curEngineer.Id == id);//checks if it exsists in the list
        if (obj is null)//if the object does not exist
            throw new Exception($"An object of type Engineer with ID {id} does not exist");
        DataSource.Engineers.Remove(obj);//remove from the list
        Engineer.counterEngineers--;//subtract 1 from the counter
    }

    public Engineer? Read(int id)//get id of an engineer to read
    {
        return DataSource.Engineers.Find(curEngineer => curEngineer.Id == id);//find the engineer in the list
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);//return a copy of the list
    }

    public void Update(Engineer item)//get an engineer to update
    {
        Engineer? obj = DataSource.Engineers.Find(curEngineer => curEngineer.Id == item.Id);//find the object in the list
        if (obj is null)//if does not exist in the list
            throw new Exception($"An object of type Engineer with ID {item.Id} does not exist");
        DataSource.Engineers.Remove(obj);//delete the old engineer
        DataSource.Engineers.Add(item);//add the updated one
    }
}
