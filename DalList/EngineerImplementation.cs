
namespace Dal;

using DalApi;
using DO;

using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        Engineer? obj = DataSource.Engineers.Find(curEngineer => curEngineer.Id == item.Id);
        if (obj is not null)
            throw new Exception($"An object of type Engineer with ID {item.Id} already exists");
        DataSource.Engineers.Add(item);
        Engineer.counterEngineers++;
        return item.Id;
}

public void Delete(int id)
    {
        Engineer? obj = DataSource.Engineers.Find(curEngineer => curEngineer.Id == id);
        if (obj == null)
            throw new Exception($"An object of type Engineer with ID {id} does not exist");
        DataSource.Engineers.Remove(obj);
        Engineer.counterEngineers--;
    }

    public Engineer? Read(int id)
    {
        return DataSource.Engineers.Find(curEngineer => curEngineer.Id == id);
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Engineer? obj = DataSource.Engineers.Find(curEngineer => curEngineer.Id == item.Id);
        if (obj == null)
            throw new Exception($"An object of type Engineer with ID {item.Id} does not exist");
        DataSource.Engineers.Remove(obj);
        DataSource.Engineers.Add(item);
    }
}
