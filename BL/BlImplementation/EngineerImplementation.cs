
namespace BlImplementation;
using BlApi;
using BO;
using System;


internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer
        (boEngineer.Id,
        boEngineer.Name, 
        boEngineer.Email,
        (DO.EngineerExperience)boEngineer.Level,
        boEngineer.Cost);
        try
        {
            int idEng = _dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException exception)
        {
            throw new BO.BlAlreadyExistsException($"An object of type Engineer with ID {boEngineer.Id} already exists", exception);
        }

    }


    public void Delete(int id)
    {
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"An object of type Engineer with ID {id} does not exist", exception);
        }
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"An object of type Engineer with ID {id} does not exist");
        var tasks = _dal.Task.ReadAll();
        var engTask = (from task in tasks
                           where task.EngineerId==id && task.Start is not null&& task.Complete is null
                           select new { task.Id, task.Alias }).FirstOrDefault();
        TaskInEngineer? taskInEngineer= null;
        if (engTask is not null)
            taskInEngineer = new TaskInEngineer() { Id=engTask.Id, Alias=engTask.Alias };
        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            Task = taskInEngineer
        };
    }

    public BO.Engineer? Read(Func<BO.Engineer, bool> filter)
    {
         return ReadAll(filter).FirstOrDefault();
    }


    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        var doEngList = _dal.Engineer.ReadAll();
        List<BO.Engineer?> boEngList = new List<BO.Engineer?>();
        foreach (var engineer in doEngList)
        {
            boEngList.Add(Read(engineer!.Id)!);
        }
        return boEngList;
    }


    public void Update(BO.Engineer boEngineer)
    {
        //בעדכון של משימה לבדוק אם צריך לעדכן את המהנדס
        DO.Engineer doEngineer = new DO.Engineer
       (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"An object of type Engineer with ID {boEngineer.Id} does not exist", exception);
        }
    }
}
