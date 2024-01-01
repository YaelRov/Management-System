using DalApi;
using DO;
using System.ComponentModel;
using System.Xml.Linq;

namespace Dal;
/// <summary>
/// class Dependency Implementation, by XElement
/// </summary>
internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// gets a dependency object and add it to the file
    /// </summary>
    /// <param name="item">object type Dependency</param>
    /// <returns>id of the dependency, type int</returns>
    public int Create(Dependency item)
    {
        XElement? xmlDependenciesFileRoot = XMLTools.LoadListFromXMLElement("dependencies");
        //creating the new "Dependency" element
        int nextId = Config.NextDependencyId;
        XElement newDep = new XElement("Dependency",
                                        new XElement("Id", item.Id == -1 ? nextId : item.Id),
                                        item.DependentTask is not null ? new XElement("DependentTask", item.DependentTask) : null,
                                        item.DependsOnTask is not null ? new XElement("DependsOnTask", item.DependsOnTask) : null
                                        );
        xmlDependenciesFileRoot.Add(newDep);
        XMLTools.SaveListToXMLElement(xmlDependenciesFileRoot, "dependencies");
        Dependency.counterDependencies++;//add 1 to the counter of the dependencies
        return nextId;
    }
    /// <summary>
    /// gets an id number of a dependency and delete it out from the file
    /// </summary>
    /// <param name="id">int</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        Dependency? foundDep = Read(id);
        if (foundDep is null)//if the object does not exist
            throw new DalDoesNotExistException($"An object of type Dependency with ID {id} does not exist");

        XElement? xmlDependenciesFileRoot = XMLTools.LoadListFromXMLElement("dependencies");
        xmlDependenciesFileRoot.Descendants("Dependency")
                    .First(dep => Convert.ToInt32(dep.Element("Id")!.Value).Equals(id))
                    .Remove();
        XMLTools.SaveListToXMLElement(xmlDependenciesFileRoot, "dependencies");
        Dependency.counterDependencies--;//remove 1 from the counter of the dependencies
    }
    /// <summary>
    /// get id of a dependency to read
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>an object type Dependency</returns>
    public Dependency? Read(int id)
    {
        XElement? xmlDependencies = XMLTools.LoadListFromXMLElement("dependencies");
        XElement? dep = xmlDependencies.Descendants("Dependency")
            .FirstOrDefault(Dependency => int.Parse(Dependency.Element("Id")!.Value).Equals(id));
        if (dep is null)
            return null;
        Dependency returnedDependency = new(int.Parse(dep.Element("Id")!.Value),
                                        dep.Element("DependentTask") is not null ? int.Parse(dep.Element("DependentTask")!.Value) : null,
                                        dep.Element("DependsOnTask") is not null ? int.Parse(dep.Element("DependsOnTask")!.Value) : null);
        return returnedDependency;
    }
    /// <summary>
    /// get condition of a dependency to read
    /// </summary>
    /// <param name="filter">a condition function</param>
    /// <returns>the first element that satisfies the conditin</returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        XElement? xmlDependencies = XMLTools.LoadListFromXMLElement("dependencies");
        XElement? dep = xmlDependencies.Descendants("Dependency")
            .FirstOrDefault(depend => filter(new(int.Parse(depend.Element("Id")!.Value),
                                        depend.Element("DependentTask") is not null ? int.Parse(depend.Element("DependentTask")!.Value) : null,
                                        depend.Element("DependsOnTask") is not null ? int.Parse(depend.Element("DependsOnTask")!.Value) : null)));
        if (dep is null)
            return null;
        Dependency returnedDependency = new(int.Parse(dep.Element("Id")!.Value),
                                        dep.Element("DependentTask") is not null ? int.Parse(dep.Element("DependentTask")!.Value) : null,
                                        dep.Element("DependsOnTask") is not null ? int.Parse(dep.Element("DependsOnTask")!.Value) : null);
        return returnedDependency;
    }
    /// <summary>
    /// reading all the file of the dependencies
    /// </summary>
    /// <returns>IEnumerable of type Dependency?</returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        XElement? xmlDependencies = XMLTools.LoadListFromXMLElement("dependencies");
        if (filter is null)
            filter = (e) => true;
        List<Dependency> DependenciesList = xmlDependencies.Descendants("Dependency")
            .Select(dep =>
            {
                Dependency dependency_t = new(
                                        int.Parse(dep.Element("Id")!.Value),
                                        dep.Element("DependentTask") is not null ? int.Parse(dep.Element("DependentTask")!.Value) : null,
                                        dep.Element("DependsOnTask") is not null ? int.Parse(dep.Element("DependsOnTask")!.Value) : null);
                return dependency_t;
            })
            .Where(dep => filter(dep))
            .ToList();
        return DependenciesList;
    }
    /// <summary>
    /// updating a dependency
    /// </summary>
    /// <param name="item">an object type Dependency</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Dependency item)
    {
        //find the object in the list
        Dependency? foundDep = Read(item.Id);
        if (foundDep is null)//if does not exist in the list
            throw new DalDoesNotExistException($"An object of type Dependency with ID {item.Id} does not exist");
        Delete(item.Id);
        Create(item);
    }
    /// <summary>
    /// reset the repository
    /// </summary>
    public void Reset()
    {
        XElement root = XMLTools.LoadListFromXMLElement("dependencies");
        root.Descendants("Dependency").Remove();
        XMLTools.SaveListToXMLElement(root, "dependencies");
    }
}
