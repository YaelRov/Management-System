
namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;
        Task newTask = new Task(newId,item.Description, item.Alias, item.Milestone, item.CreatedAt, item.Start, item.ScheduledDate, item.ForecastDate, item.Deadline, item.Complete, item.Deliverables, item.Remarks, item.EngineerId, item.ComplexityLevel);
        DataSource.Tasks.Add(newTask);
        Task.counterTasks++;
        return newId;
    }

    public void Delete(int id)
    {
        Task? obj = DataSource.Tasks.Find(curTask => curTask.Id == id);
        if (obj is null)
            throw new Exception($"An object of type Task with ID {id} does not exist");
        DataSource.Tasks.Remove(obj);
        Task.counterTasks++;
    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(curTask => curTask.Id == id);
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        Task? obj = DataSource.Tasks.Find(curTask => curTask.Id == item.Id);
        if (obj is null)
            throw new Exception($"An object of type Task with ID {item.Id} does not exist");
        DataSource.Tasks.Remove(obj);
        DataSource.Tasks.Add(item);
    }
}
