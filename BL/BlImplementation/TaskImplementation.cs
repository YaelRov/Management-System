
namespace BlImplementation;
using BlApi;
/// <summary>
/// class for the implementations of task methods
/// </summary>
internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// Creates new entity object of Task
    /// </summary>
    /// <param name="boEngineer">task of type BO.Task</param>
    /// <returns>the created task id</returns>
    /// <exception cref="BO.BlAlreadyExistsException"></exception>
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

    /// <summary>
    /// Deletes an task by its Id
    /// </summary>
    /// <param name="id">id of task, type int</param>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
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


    /// <summary>
    /// Reads task by its ID 
    /// </summary>
    /// <param name="id">task id, type int</param>
    /// <returns>the task entity</returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
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

    /// <summary>
    /// Reads task according to a condition
    /// </summary>
    /// <param name="filter">condition</param>
    /// <returns>the task entity</returns>
    public BO.Task? Read(Func<BO.Task, bool> filter)
    {
        return ReadAll(filter).FirstOrDefault();
    }


    /// <summary>
    /// Reads all tasks
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>IEnumerable<BO.TaskEngineer?></returns>
    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        var doTaskList = _dal.Task.ReadAll();
        List<BO.Task?> boTaskList = new List<BO.Task?>();
        foreach (var task in doTaskList)
        {
            int id = task!.Id;
            boTaskList.Add(Read(id)!);
        }
        return boTaskList;
    }

    /// <summary>
    /// Updates task
    /// </summary>
    /// <param name="boTask"></param>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
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
}
