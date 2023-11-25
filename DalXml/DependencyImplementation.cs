using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class DependencyImplementation : IDependency
{

    public int Create(Dependency item)
    {

        //checking if this Dependency is exists already
        Dependency? isExistDependency = Read(item.Id);
        if (isExistDependency is not null)
            throw new DalAlreadyExistsException($"An object of type Dependency with ID {item.Id} already exists");
        XElement? xmlDependenciesFileRoot = XMLTools.LoadListFromXMLElement("dependencies");
        //checking if the root element "Dependencies" exists
        XElement? xmlDependencies = xmlDependenciesFileRoot.Descendants("Dependencies").FirstOrDefault();
        //creating the root element "Dependencies" if it wasn't exist
        xmlDependencies ??= new XElement("Dependencies");

        //adding the new "Dependency" element
        xmlDependencies!.Add(new XElement("Dependency",
                                        new XAttribute("DependentTask", item.DependentTask??0),
                                        new XAttribute("DependsOnTask", item.DependsOnTask??0),
                                        item.Id));
        XMLTools.SaveListToXMLElement(xmlDependencies, "Dependencies");
        Dependency.counterDependencies++;//add 1 to the counter of the dependencies
        return item.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        XElement? xmlDependencies = XMLTools.LoadListFromXMLElement("dependencies");
        XElement? dep = xmlDependencies.Descendants("Dependencies")
            .FirstOrDefault(Dependency => int.Parse(Dependency.Attribute("Id")!.Value).Equals(id));
        if (dep is null)
            return null;
        Dependency returnedDependency = new(int.Parse(dep!.Value),
                                        int.Parse(dep.Attribute("DependentTask")!.Value),
                                        int.Parse(dep.Attribute("DependsOnTask")!.Value));
        return returnedDependency;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        XElement? xmlDependencies = XMLTools.LoadListFromXMLElement("dependencies");
        if (filter is null)
            filter = (e) => true;
        List<Dependency> DependenciesList = xmlDependencies.Descendants("dependencies")
            .Select(dep => {
                Dependency dependency_t = new(int.Parse(dep!.Value),
                                        int.Parse(dep.Attribute("DependentTask")!.Value),
                                        int.Parse(dep.Attribute("DependsOnTask")!.Value));
                return dependency_t;
            })
            .Where(dep => filter(dep))
            .ToList();
        return DependenciesList;
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
