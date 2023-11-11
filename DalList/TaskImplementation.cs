
namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)//gets a task object and add it to the list
    {
        int newId = DataSource.Config.NextTaskId;//get a barcode from the config class
        Task newTask = new Task(newId,item.Description, item.Alias, item.Milestone, item.CreatedAt, item.Start, item.ScheduledDate, item.ForecastDate, item.Deadline, item.Complete, item.Deliverables, item.Remarks, item.EngineerId, item.ComplexityLevel);//create a new task object
        DataSource.Tasks.Add(newTask);//add the task object to the list
        Task.counterTasks++;//add 1 to the counter of the tasks
        return newId;//return the id of the task
    }

    public void Delete(int id)// gets an id number of a task and delete it out from the list
    {
        Task? obj = DataSource.Tasks.Find(curTask => curTask.Id == id);//checks if it exsists in the list
        if (obj is null)//if the object does not exist
            throw new Exception($"An object of type Task with ID {id} does not exist");
        DataSource.Tasks.Remove(obj);//remove from the list
        Task.counterTasks--;//subtract 1 from the counter
    }

    public Task? Read(int id)//get id of a task to read
    {
        return DataSource.Tasks.Find(curTask => curTask.Id == id);//find the task in the list
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);//return a copy of the list
    }

    public void Update(Task item)//get a task to update
    {
        Task? obj = DataSource.Tasks.Find(curTask => curTask.Id == item.Id);//find the object in the list
        if (obj is null)//if does not exist in the list
            throw new Exception($"An object of type Task with ID {item.Id} does not exist");
        DataSource.Tasks.Remove(obj);//delete the old task
        DataSource.Tasks.Add(item);//add the updated one
    }
}
