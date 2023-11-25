using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class TaskImplementation : ITask
{
    public int Create(DO.Task item)
    {
        //checking if this Task is exists already
        DO.Task? isExistTask = Read(item.Id);
        if (isExistTask is not null)
            throw new DalAlreadyExistsException($"An object of type Task with ID {item.Id} already exists");
        XElement? xmlTasksFileRoot = XMLTools.LoadListFromXMLElement("tasks");
        //checking if the root element "Tasks" exists
        XElement? xmlTasks = xmlTasksFileRoot.Descendants("Tasks").FirstOrDefault();
        //creating the root element "Tasks" if it wasn't exist
        xmlTasks ??= new XElement("Tasks");
        //adding the new "Task" element
        xmlTasks!.Add(new XElement("Task",
                                        new XAttribute("Id", item.Id),
                                        new XAttribute("Alias", item.Alias),
                                        new XAttribute("Milestone", item.Milestone),
                                        new XAttribute("CreatedAt", item.CreatedAt),
                                        new XAttribute("Start", item.Start??new DateTime()),
                                        new XAttribute("ScheduledDate", item.ScheduledDate ?? new DateTime()),
                                        new XAttribute("ForecastDate", item.ForecastDate ?? new DateTime()),
                                        new XAttribute("Deadline", item.Deadline ?? new DateTime()),
                                        new XAttribute("Complete", item.Complete ?? new DateTime()),
                                        new XAttribute("Deliverables", item.Deliverables ?? ""),
                                        new XAttribute("Remarks", item.Remarks ?? ""),
                                        new XAttribute("TaskId", item.EngineerId ?? 0),
                                        new XAttribute("ComplexityLevel", item.ComplexityLevel ?? (EngineerExperience)Enum.Parse(typeof(EngineerExperience),"Novice")),
                                        item.Description));
        XMLTools.SaveListToXMLElement(xmlTasks, "Tasks");
        DO.Task.counterTasks++;//add 1 to the counter of the tasks
        return item.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Task? Read(int id)
    {
        XElement? xmlTasks = XMLTools.LoadListFromXMLElement("tasks");
        XElement? task = xmlTasks.Descendants("Tasks")
            .FirstOrDefault(Task => int.Parse(Task.Attribute("Id")!.Value).Equals(id));
        if (task is null)
            return null;
        DO.Task returnedTask = new(int.Parse(task.Attribute("Id")!.Value),
                                        task.Value,
                                        task.Attribute("Alias")!.Value,
                                        Convert.ToBoolean(task.Attribute("Milestone")!.Value),
                                        Convert.ToDateTime(task.Attribute("CreatedAt")!.Value),
                                        Convert.ToDateTime(task.Attribute("Start")!.Value),
                                        Convert.ToDateTime(task.Attribute("ScheduledDate")!.Value),
                                        Convert.ToDateTime(task.Attribute("ForecastDate")!.Value),
                                        Convert.ToDateTime(task.Attribute("Deadline")!.Value),
                                        Convert.ToDateTime(task.Attribute("Complete")!.Value),
                                        task.Attribute("Deliverables")!.Value,
                                        task.Attribute("Remarks")!.Value,
                                        Convert.ToInt32(task.Attribute("EngineerId")!.Value),
                                        (EngineerExperience)Enum.Parse(typeof(EngineerExperience), task.Attribute("ComplexityLevel")!.Value));
        return returnedTask;
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        XElement? xmlTasks = XMLTools.LoadListFromXMLElement("tasks");
        if (filter is null)
            filter = (e) => true;
        List<DO.Task> TasksList = xmlTasks.Descendants("Tasks")
            .Select(task => {
                DO.Task Task_t = new(int.Parse(task.Attribute("Id")!.Value),
                                        task.Value,
                                        task.Attribute("Alias")!.Value,
                                        Convert.ToBoolean(task.Attribute("Milestone")!.Value),
                                        Convert.ToDateTime(task.Attribute("CreatedAt")!.Value),
                                        Convert.ToDateTime(task.Attribute("Start")!.Value),
                                        Convert.ToDateTime(task.Attribute("ScheduledDate")!.Value),
                                        Convert.ToDateTime(task.Attribute("ForecastDate")!.Value),
                                        Convert.ToDateTime(task.Attribute("Deadline")!.Value),
                                        Convert.ToDateTime(task.Attribute("Complete")!.Value),
                                        task.Attribute("Deliverables")!.Value,
                                        task.Attribute("Remarks")!.Value,
                                        Convert.ToInt32(task.Attribute("EngineerId")!.Value),
                                        (EngineerExperience)Enum.Parse(typeof(EngineerExperience), task.Attribute("ComplexityLevel")!.Value));
                return Task_t;
            })
            .Where(task => filter(task))
            .ToList();
        return TasksList;
    }

    public void Update(DO.Task item)
    {
        throw new NotImplementedException();
    }
}
