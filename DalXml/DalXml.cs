
using DalApi;
using DO;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Xml.Linq;

namespace Dal;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

    DateTime? IDal.StartProjectDate { 
        get{
            XElement root = XMLTools.LoadListFromXMLElement("data-config");
            return root.Element("Config")!.ToDateTimeNullable("startProjectDate");
        }
        set
        {
            XElement root = XMLTools.LoadListFromXMLElement("data-config");
            root.Descendants("startProjectDate").First().SetValue(value ?? throw new DalDoesNotExistException("Date of start project can't be null\n"));
            XMLTools.SaveListToXMLElement(root, "data-config");
        }
    }
    DateTime? IDal.EndProjectDate { 
        get
        {
            XElement root = XMLTools.LoadListFromXMLElement("data-config");
            return root.Element("Config")!.ToDateTimeNullable("endProjectDate");
        }
        set {
            XElement root = XMLTools.LoadListFromXMLElement("data-config");
            root.Descendants("endProjectDate").First().SetValue(value ?? throw new DalDoesNotExistException("Date of end project can't be null\n"));
            XMLTools.SaveListToXMLElement(root, "data-config");
        }
    }

    public void Reset()
    {
        XMLTools.ResetFile("engineers");
        XMLTools.ResetFile("tasks");
        XMLTools.ResetFile("dependencies");
    }
}
