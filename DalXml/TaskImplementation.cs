
namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Xml.Linq;

/// <summary>
/// class Task Implementation, by XElement
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
        XElement? xmlTasksFileRoot = XMLTools.LoadListFromXMLElement("tasks");
        //creating the new "Task" element
        int nextId = Config.NextTaskId;
        XElement newTask = new XElement("Task",
                                        new XElement("Id", item.Id == -1 ? nextId : item.Id),
                                        new XElement("Description", item.Description),
                                        new XElement("Alias", item.Alias),
                                        new XElement("Milestone", Convert.ToBoolean(item.Milestone)),
                                        new XElement("CreatedAt", Convert.ToDateTime(item.CreatedAt)),
                                        item.RequiredEffortTime is not null ? new XElement("RequiredEffortTime", new TimeSpan( Convert.ToInt32(item.RequiredEffortTime),0,0,0) ): null,
                                        item.Start is not null ? new XElement("Start", Convert.ToDateTime(item.Start)) : null,
                                        item.ScheduledDate is not null ? new XElement("ScheduledDate", Convert.ToDateTime(item.ScheduledDate)) : null,
                                        item.Deadline is not null ? new XElement("Deadline", Convert.ToDateTime(item.Deadline)) : null,
                                        item.Complete is not null ? new XElement("Complete", Convert.ToDateTime(item.Complete)) : null,
                                        item.Deliverables is not null ? new XElement("Deliverables", item.Deliverables) : null,
                                        item.Remarks is not null ? new XElement("Remarks", item.Remarks) : null,
                                        item.EngineerId is not null ? new XElement("EngineerId", Convert.ToInt32(item.EngineerId)) : null,
                                        item.ComplexityLevel is not null ? new XElement("ComplexityLevel", (EngineerExperience)item.ComplexityLevel): null
                                        );
        xmlTasksFileRoot.Add(newTask);
        XMLTools.SaveListToXMLElement(xmlTasksFileRoot, "tasks");
        Task.counterTasks++;//add 1 to the counter of the tasks
        return nextId;


    }
    /// <summary>
    /// gets an id number of a task and delete it out from the file
    /// </summary>
    /// <param name="id">int</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        Task? foundTask = Read(id);
        if (foundTask is null)//if the object does not exist
            throw new DalDoesNotExistException($"An object of type Task with ID {id} does not exist");

        XElement? xmlTasksFileRoot = XMLTools.LoadListFromXMLElement("tasks");
        xmlTasksFileRoot.Descendants("Task")
                    .First(task => Convert.ToInt32(task.Element("Id")!.Value).Equals(id))
                    .Remove();
        XMLTools.SaveListToXMLElement(xmlTasksFileRoot, "tasks");
        Task.counterTasks--;//remove 1 from the counter of the dependencies
    }
    /// <summary>
    /// get id of a task to read
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>an object type Task</returns>
    public Task? Read(int id)
    {
        XElement? xmlTasks = XMLTools.LoadListFromXMLElement("tasks");
        XElement? task = xmlTasks.Descendants("Task")
            .FirstOrDefault(Task => int.Parse(Task.Element("Id")!.Value).Equals(id));
        if (task is null)
            return null;
        Task returnedTask = new (
                                int.Parse(task.Element("Id")!.Value),
                                task.Element("Description")!.Value,
                                task.Element("Alias")!.Value,
                                Convert.ToBoolean(task.Element("Milestone")!.Value),
                                Convert.ToDateTime(task.Element("CreatedAt")!.Value),
                                task.Element("RequiredEffortTime") is not null ? new TimeSpan(Convert.ToInt32(task.Element("RequiredEffortTime")!.Value),0,0,0) : null,
                                task.Element("Start") is not null ? Convert.ToDateTime(task.Element("Start")!.Value) : null,
                                task.Element("ScheduledDate") is not null ? Convert.ToDateTime(task.Element("ScheduledDate")!.Value) : null,
                                task.Element("Deadline") is not null ? Convert.ToDateTime(task.Element("Deadline")!.Value) : null,
                                task.Element("Complete") is not null ? Convert.ToDateTime(task.Element("Complete")!.Value) : null,
                                task.Element("Deliverables") is not null ? task.Element("Deliverables")!.Value : null,
                                task.Element("Remarks") is not null ? task.Element("Remarks")!.Value : null,
                                task.Element("EngineerId") is not null ? Convert.ToInt32(task.Element("EngineerId")!.Value) : null,
                                task.Element("ComplexityLevel") is not null ? (EngineerExperience)Enum.Parse(typeof(EngineerExperience), task.Element("ComplexityLevel")!.Value) : null
                                );
        return returnedTask;
    }
    /// <summary>
    /// get condition of a task to read
    /// </summary>
    /// <param name="filter">a condition function</param>
    /// <returns>the first element that satisfies the conditin</returns>
    public Task? Read(Func<Task, bool> filter) //stage 2
    {
        XElement? xmlTasks = XMLTools.LoadListFromXMLElement("tasks");
        XElement? task = xmlTasks.Descendants("Task")
            .FirstOrDefault(task => filter(new(
                                int.Parse(task.Element("Id")!.Value),
                                task.Element("Description")!.Value,
                                task.Element("Alias")!.Value,
                                Convert.ToBoolean(task.Element("Milestone")!.Value),
                                Convert.ToDateTime(task.Element("CreatedAt")!.Value),
                                task.Element("RequiredEffortTime") is not null ? new TimeSpan(Convert.ToInt32(task.Element("RequiredEffortTime")!.Value), 0, 0, 0) : null,
                                task.Element("Start") is not null ? Convert.ToDateTime(task.Element("Start")!.Value) : null,
                                task.Element("ScheduledDate") is not null ? Convert.ToDateTime(task.Element("ScheduledDate")!.Value) : null,
                                task.Element("Deadline") is not null ? Convert.ToDateTime(task.Element("Deadline")!.Value) : null,
                                task.Element("Complete") is not null ? Convert.ToDateTime(task.Element("Complete")!.Value) : null,
                                task.Element("Deliverables") is not null ? task.Element("Deliverables")!.Value : null,
                                task.Element("Remarks") is not null ? task.Element("Remarks")!.Value : null,
                                task.Element("EngineerId") is not null ? Convert.ToInt32(task.Element("EngineerId")!.Value) : null,
                                task.Element("ComplexityLevel") is not null ? (EngineerExperience)Enum.Parse(typeof(EngineerExperience), task.Element("ComplexityLevel")!.Value) : null))
                                );
        if (task is null)
            return null;
        Task returnedTask = new(
                                int.Parse(task.Element("Id")!.Value),
                                task.Element("Description")!.Value,
                                task.Element("Alias")!.Value,
                                Convert.ToBoolean(task.Element("Milestone")!.Value),
                                Convert.ToDateTime(task.Element("CreatedAt")!.Value),
                                task.Element("RequiredEffortTime") is not null ? new TimeSpan(Convert.ToInt32(task.Element("RequiredEffortTime")!.Value), 0, 0, 0) : null,
                                task.Element("Start") is not null ? Convert.ToDateTime(task.Element("Start")!.Value) : null,
                                task.Element("ScheduledDate") is not null ? Convert.ToDateTime(task.Element("ScheduledDate")!.Value) : null,
                                task.Element("Deadline") is not null ? Convert.ToDateTime(task.Element("Deadline")!.Value) : null,
                                task.Element("Complete") is not null ? Convert.ToDateTime(task.Element("Complete")!.Value) : null,
                                task.Element("Deliverables") is not null ? task.Element("Deliverables")!.Value : null,
                                task.Element("Remarks") is not null ? task.Element("Remarks")!.Value : null,
                                task.Element("EngineerId") is not null ? Convert.ToInt32(task.Element("EngineerId")!.Value) : null,
                                task.Element("ComplexityLevel") is not null ? (EngineerExperience)Enum.Parse(typeof(EngineerExperience), task.Element("ComplexityLevel")!.Value) : null
                                );
        return returnedTask;
    }
    /// <summary>
    /// reading all the file of the tasks
    /// </summary>
    /// <returns>IEnumerable of type Task?</returns>

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null) //stage 2
    {
        XElement? xmlTasks = XMLTools.LoadListFromXMLElement("tasks");
        if (filter is null)
            filter = (e) => true;
        List<Task> TaskList = xmlTasks.Descendants("Task")
            .Select(task =>
            {
                Task task_t = new(
                                int.Parse(task.Element("Id")!.Value),
                                task.Element("Description")!.Value,
                                task.Element("Alias")!.Value,
                                Convert.ToBoolean(task.Element("Milestone")!.Value),
                                Convert.ToDateTime(task.Element("CreatedAt")!.Value),
                                task.Element("RequiredEffortTime") is not null ? new TimeSpan(Convert.ToInt32(task.Element("RequiredEffortTime")!.Value), 0, 0, 0) : null,
                                task.Element("Start") is not null ? Convert.ToDateTime(task.Element("Start")!.Value) : null,
                                task.Element("ScheduledDate") is not null ? Convert.ToDateTime(task.Element("ScheduledDate")!.Value) : null,
                                task.Element("Deadline") is not null ? Convert.ToDateTime(task.Element("Deadline")!.Value) : null,
                                task.Element("Complete") is not null ? Convert.ToDateTime(task.Element("Complete")!.Value) : null,
                                task.Element("Deliverables") is not null ? task.Element("Deliverables")!.Value : null,
                                task.Element("Remarks") is not null ? task.Element("Remarks")!.Value : null,
                                task.Element("EngineerId") is not null ? Convert.ToInt32(task.Element("EngineerId")!.Value) : null,
                                task.Element("ComplexityLevel") is not null ? (EngineerExperience)Enum.Parse(typeof(EngineerExperience), task.Element("ComplexityLevel")!.Value) : null
                                );
                return task_t;
            })
            .Where(task => filter(task))
            .ToList();
        return TaskList;
    }
    /// <summary>
    /// updating a task
    /// </summary>
    /// <param name="item">an object type Task</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Task item)
    {
        //find the object in the file
        Task? foundTask = Read(item.Id);
        if (foundTask is null)//if does not exist in the file
            throw new DalDoesNotExistException($"An object of type Task with ID {item.Id} does not exist");
        Delete(item.Id);
        Create(item);
    }
}
