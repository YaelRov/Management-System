
namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class TaskImplementation : ITask
{
    /// <summary>
    /// gets a task object and add it to the list
    /// </summary>
    /// <param name="item">an object type Task</param>
    /// <returns>the id of the task, type int</returns>
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;//get a barcode from the config class
        Task newTask = new Task(newId,item.Description, item.Alias, item.Milestone, item.CreatedAt, item.Start, item.ScheduledDate, item.ForecastDate, item.Deadline, item.Complete, item.Deliverables, item.Remarks, item.EngineerId, item.ComplexityLevel);//create a new task object
        DataSource.Tasks.Add(newTask);//add the task object to the list
        Task.counterTasks++;//add 1 to the counter of the tasks
        return newId;
    }
    /// <summary>
    /// gets an id number of a task and delete it out from the list
    /// </summary>
    /// <param name="id">int</param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        Task? foundTask = Read(id);
        if (foundTask is null)//if the object does not exist
            throw new DalDoesNotExistException($"An object of type Task with ID {id} does not exist");
        DataSource.Tasks.Remove(foundTask);//remove from the list
        Task.counterTasks--;//subtract 1 from the counter
    }
    /// <summary>
    /// get id of a task to read
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>an object type Task</returns>
    public Task? Read(int id)
    {
        //find the task in the list
        var foundTask = DataSource.Tasks
                     .FirstOrDefault(curTask => curTask.Id == id);
        return foundTask;
    }
    public Task? Read(Func<Task, bool> filter) //stage 2
    {
        return DataSource.Tasks
              .FirstOrDefault(filter);
    }
    /// <summary>
    /// reading all the list of the tasks
    /// </summary>
    /// <returns>copy of the tasks list</returns>

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Tasks.Select(item => item);
        else
            return DataSource.Tasks.Where(filter);
    }
    /// <summary>
    /// updating a task
    /// </summary>
    /// <param name="item">an object type Task</param>
    /// <exception cref="Exception"></exception>
    public void Update(Task item)
    {
        //find the object in the list
        Task? foundTask = Read(item.Id);
        if (foundTask is null)//if does not exist in the list
            throw new DalDoesNotExistException($"An object of type Task with ID {item.Id} does not exist");
        DataSource.Tasks.Remove(foundTask);//delete the old task
        DataSource.Tasks.Add(item);//add the updated one
    }
}
