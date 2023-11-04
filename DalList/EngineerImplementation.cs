
namespace Dal;

using DalApi;
using DO;

using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        Engineer obj = DataSource.Engineers.Find(curEngineer => curEngineer.Id == item.Id);
        if (obj == null)
            throw new NotImplementedException("An object of type Engineer with this ID already exists");
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        Engineer obj = DataSource.Engineers.Find(curEngineer => curEngineer.Id == id);
        if (obj == null)
            throw new NotImplementedException("An object of type Engineer with this ID does not exist");
        DataSource.Engineers.Remove(obj);
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
        Engineer obj = DataSource.Engineers.Find(curEngineer => curEngineer.Id == item.Id);
        if (obj == null)
            throw new NotImplementedException("An object of type Engineer with this ID does not exist");
        DataSource.Engineers.Remove(obj);
        DataSource.Engineers.Add(item);
    }
}
