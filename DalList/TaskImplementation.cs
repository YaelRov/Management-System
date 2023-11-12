
namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

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
        Task? obj = DataSource.Tasks.Find(curTask => curTask.Id == id);//checks if it exsists in the list
        if (obj is null)//if the object does not exist
            throw new Exception($"An object of type Task with ID {id} does not exist");
        DataSource.Tasks.Remove(obj);//remove from the list
        Task.counterTasks--;//subtract 1 from the counter
    }
    /// <summary>
    /// get id of a task to read
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>an object type Task</returns>
    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(curTask => curTask.Id == id);//find the task in the list
    }
    /// <summary>
    /// reading all the list of the tasks
    /// </summary>
    /// <returns>copy of the tasks list</returns>
    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }
    /// <summary>
    /// updating a task
    /// </summary>
    /// <param name="item">an object type Task</param>
    /// <exception cref="Exception"></exception>
    public void Update(Task item)
    {
        Task? obj = DataSource.Tasks.Find(curTask => curTask.Id == item.Id);//find the object in the list
        if (obj is null)//if does not exist in the list
            throw new Exception($"An object of type Task with ID {item.Id} does not exist");
        DataSource.Tasks.Remove(obj);//delete the old task
        DataSource.Tasks.Add(item);//add the updated one
    }
}
