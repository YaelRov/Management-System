using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class DependencyImplementation : IDependency
{

    public int Create(Dependency item)
    {
        XElement? xmlDependenciesFileRoot = XMLTools.LoadListFromXMLElement("dependencies");
        //creating the new "Dependency" element
        XElement newDep = new XElement("Dependency",
                                        new XElement("Id", Config.NextDependencyId),
                                        new XElement("DependentTask", item.DependentTask ?? 0),
                                        new XElement("DependsOnTask", item.DependsOnTask ?? 0)
                                        );
        xmlDependenciesFileRoot.Add(newDep);
        XMLTools.SaveListToXMLElement(xmlDependenciesFileRoot, "dependencies");
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
        List<Dependency> DependenciesList = xmlDependencies.Descendants("Dependency")
            .Select(dep => {
                Dependency dependency_t = new(
                                        int.Parse(dep.Element("Id")!.Value),
                                        int.Parse(dep.Element("DependentTask")!.Value),
                                        int.Parse(dep.Element("DependsOnTask")!.Value));
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
