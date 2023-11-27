
namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;
/// <summary>
/// class Task Implementation, by Serializer
/// </summary>
internal class TaskImplementation : ITask
{
    /// <summary>
    /// gets a task object and add it to the file
    /// </summary>
    /// <param name="item">an object type Task</param>
    /// <returns>the id of the task, type int</returns>
    public int Create(Task item)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        int newId = Config.NextTaskId;//get a barcode from the config class
        Task newTask = new Task(newId, item.Description, item.Alias, item.Milestone, item.CreatedAt, item.Start, item.ScheduledDate, item.ForecastDate, item.Deadline, item.Complete, item.Deliverables, item.Remarks, item.EngineerId, item.ComplexityLevel);//create a new task object
        tasksList.Add(newTask);//add the task object to the file
        XMLTools.SaveListToXMLSerializer<Task>(tasksList, "tasks");
        Task.counterTasks++;//add 1 to the counter of the tasks
        return newId;
    }
    /// <summary>
    /// gets an id number of a task and delete it out from the file
    /// </summary>
    /// <param name="id">int</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? foundTask = Read(id);
        if (foundTask is null)//if the object does not exist
            throw new DalDoesNotExistException($"An object of type Task with ID {id} does not exist");
        tasksList.Remove(foundTask);//remove from the file
        XMLTools.SaveListToXMLSerializer<Task>(tasksList, "tasks");
        Task.counterTasks--;//subtract 1 from the counter
    }
    /// <summary>
    /// get id of a task to read
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>an object type Task</returns>
    public Task? Read(int id)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        //find the task in the file
        var foundTask = tasksList
                     .FirstOrDefault(curTask => curTask.Id == id);
        return foundTask;
    }
    /// <summary>
    /// get condition of a task to read
    /// </summary>
    /// <param name="filter">a condition function</param>
    /// <returns>the first element that satisfies the conditin</returns>
    public Task? Read(Func<Task, bool> filter) //stage 2
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return tasksList
              .FirstOrDefault(filter);
    }
    /// <summary>
    /// reading all the file of the tasks
    /// </summary>
    /// <returns>IEnumerable of type Task?</returns>

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null) //stage 2
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        if (filter == null)
            return tasksList.Select(item => item);
        else
            return tasksList.Where(filter);
    }
    /// <summary>
    /// updating a task
    /// </summary>
    /// <param name="item">an object type Task</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Task item)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        //find the object in the file
        Task? foundTask = Read(item.Id);
        if (foundTask is null)//if does not exist in the file
            throw new DalDoesNotExistException($"An object of type Task with ID {item.Id} does not exist");
        tasksList.Remove(foundTask);//delete the old task
        tasksList.Add(item);//add the updated one
        XMLTools.SaveListToXMLSerializer<Task>(tasksList, "tasks");
    }
}
