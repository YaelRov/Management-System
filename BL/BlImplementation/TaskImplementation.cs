
namespace BlImplementation;
using BlApi;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task boTask)
    {
        DO.Task doTask = new DO.Task
               (boTask.Id,
               boTask.Description,
               boTask.Alias,
               boTask.Milestone is not null ? true : false,
               boTask.CreatedAt,
               boTask.RequiredEffortTime,
               boTask.Start,
               boTask.ScheduledDate,
               boTask.Deadline,
               boTask.Complete,
               boTask.Deliverables,
               boTask.Remarks,
               boTask.Engineer is not null ? boTask.Engineer.Id : null,
               boTask.ComplexityLevel is not null ? (DO.EngineerExperience)boTask.ComplexityLevel : null);
        try
        {
            int idTask = _dal.Task.Create(doTask);
            return idTask;
        }
        catch (DO.DalAlreadyExistsException exception)
        {
            throw new BO.BlAlreadyExistsException($"An object of type Task with ID {boTask.Id} already exists", exception);
        }
    }

    public void Delete(int id)
    {
        try
        {
            _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"An object of type Task with ID {id} does not exist", exception);
        }
    }

    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"An object of type Task with ID {id} does not exist");
        DO.Engineer? eng = null;
        if (doTask.EngineerId is not null)
            eng = _dal.Engineer.Read((int)doTask.EngineerId)!;
        if (doTask.Milestone == true)
            return null;
        return new BO.Task()
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            Milestone = doTask.Milestone is true ? new BO.MilestoneInTask() : null,
            CreatedAt = doTask.CreatedAt,
            Status = (BO.Status)(doTask.ScheduledDate is null ? 0 :
                           doTask.Start is null ? 1 :
                           doTask.Complete is null ? 2
                           : 3),
            RequiredEffortTime = doTask.RequiredEffortTime,
            Start = doTask.Start,
            ScheduledDate = doTask.ScheduledDate,
            Deadline = doTask.Deadline,
            Complete = doTask.Complete,
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            Engineer = eng is not null ? new BO.EngineerInTask() { Id = eng.Id, Name = eng.Name } : null,
            ComplexityLevel = doTask.ComplexityLevel is not null ? (BO.EngineerExperience)doTask.ComplexityLevel : null
        };
    }

    public BO.Task? Read(Func<BO.Task, bool> filter)
    {
        return ReadAll(filter).FirstOrDefault();
    }

    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        var doTaskList = _dal.Task.ReadAll();
        List<BO.Task?> boTaskList = new List<BO.Task?>();
        foreach (var task in doTaskList)
        {
            boTaskList.Add(Read(task!.Id)!);
        }
        return boTaskList;
    }

    public void Update(BO.Task boTask)
    {
        DO.Task doTask = new DO.Task
             (boTask.Id,
               boTask.Description,
               boTask.Alias,
               boTask.Milestone is not null ? true : false,
               boTask.CreatedAt,
               boTask.ForecastDate - boTask.ScheduledDate,
               boTask.Start,
               boTask.ScheduledDate,
               boTask.Deadline,
               boTask.Complete,
               boTask.Deliverables,
               boTask.Remarks,
               boTask.Engineer is not null ? boTask.Engineer.Id : null,
               boTask.ComplexityLevel is not null ? (DO.EngineerExperience)boTask.ComplexityLevel : null);
        try
        {
            _dal.Task.Update(doTask);
        }
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"An object of type Task with ID {boTask.Id} does not exist", exception);
        }
    }
    public void creatD()
    {
        for (int i = 0; i < 5; i++)
        {
            DO.Task doTask = new DO.Task(0, $"{i}", "cfgv",false, DateTime.Now,null,null,null,null,null,null,null,null,null);
            _dal.Task.Create(doTask);
        }
        DO.Dependency doDependency = new DO.Dependency(0, 1002, 1001);
        _dal.Dependency.Create(doDependency);
        DO.Dependency doDependency1 = new DO.Dependency(0, 1002, 1000);
        _dal.Dependency.Create(doDependency1);
        DO.Dependency doDependency2 = new DO.Dependency(0, 1003, 1002);
        _dal.Dependency.Create(doDependency2);
        DO.Dependency doDependency3 = new DO.Dependency(0, 1004, 1002);
        _dal.Dependency.Create(doDependency3);

    }
    public void printd()
    {
        var d = _dal.Dependency.ReadAll();
        foreach (var item in d.ToList())
        {
            Console.WriteLine(item);
        }
    }
}
