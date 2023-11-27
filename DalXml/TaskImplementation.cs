using DalApi;
using DO;
using System.Collections;
using System.Linq;
using System.Xml.Linq;

namespace Dal;

internal class TaskImplementation : ITask
{
    public int Create(DO.Task item)
    {
        XElement? xmlTasksFileRoot = XMLTools.LoadListFromXMLElement("tasks");
       
        //creating the new "Task" element
        XElement newTask = new XElement("Task",
                                        item.Description,
                                        new XAttribute("Id", Config.NextTaskId),
                                        new XAttribute("Alias", item.Alias),
                                        new XAttribute("Milestone", item.Milestone),
                                        new XAttribute("CreatedAt", item.CreatedAt),
                                        new XAttribute("Start", item.Start ?? new DateTime()),
                                        new XAttribute("ScheduledDate", item.ScheduledDate ?? new DateTime()),
                                        new XAttribute("ForecastDate", item.ForecastDate ?? new DateTime()),
                                        new XAttribute("Deadline", item.Deadline ?? new DateTime()),
                                        new XAttribute("Complete", item.Complete ?? new DateTime()),
                                        new XAttribute("Deliverables", item.Deliverables ?? ""),
                                        new XAttribute("Remarks", item.Remarks ?? ""),
                                        new XAttribute("TaskId", item.EngineerId ?? 0),
                                        new XAttribute("ComplexityLevel", item.ComplexityLevel ?? (EngineerExperience)Enum.Parse(typeof(EngineerExperience), "Novice"))
                                        );
        xmlTasksFileRoot!.Add(newTask);
        XMLTools.SaveListToXMLElement(xmlTasksFileRoot, "tasks");
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
        List<DO.Task> tasksList = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");

        if (filter == null)
            return tasksList.Select(item => item).ToList();
        else
            return tasksList.Where(filter).ToList();
    }

    public void Update(DO.Task item)
    {
        throw new NotImplementedException();
    }
}
