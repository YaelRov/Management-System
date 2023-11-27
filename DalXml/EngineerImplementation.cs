
using DalApi;
using DO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        //checking if this engineer is exists already
        Engineer? isExistEngineer = Read(item.Id);
        if(isExistEngineer is not null)
            throw new DalAlreadyExistsException($"An object of type Engineer with ID {item.Id} already exists");

        XElement? xmlEngineersFileRoot = XMLTools.LoadListFromXMLElement("engineers");

        //creating the new "Engineer" element
        XElement newEng = new XElement("Engineer",
                                       item.Name,
                                       new XAttribute("Id", item.Id),
                                       new XAttribute("Email", item.Email),
                                       new XAttribute("Level", item.Level),
                                       new XAttribute("Cost", item.Cost));

        xmlEngineersFileRoot.Add(newEng);
        XMLTools.SaveListToXMLElement(xmlEngineersFileRoot, "engineers");
        Engineer.counterEngineers++;//add 1 to the counter of the engineers
        return item.Id;
    }

    public void Delete(int id)
    {
        //checking if this engineer does not exists 
        Engineer? isExistEngineer = Read(id);
        if (isExistEngineer is null)
            throw new DalDoesNotExistException($"An object of type Engineer with ID {id} does not exist");
        
    }

    public Engineer? Read(int id)
    {
        XElement? xmlEngineers = XMLTools.LoadListFromXMLElement("engineers");
        XElement? eng = xmlEngineers.Descendants("Engineers")
            .FirstOrDefault(engineer => int.Parse(engineer.Attribute("Id")!.Value).Equals(id));
        if (eng is null)
            return null;
        Engineer returnedEngineer = new(int.Parse(eng.Attribute("Id")!.Value),
                                        eng.Value, 
                                        eng.Attribute("Email")!.Value,
                                        (EngineerExperience)Enum.Parse(typeof(EngineerExperience),eng.Attribute("Level")!.Value),
                                        double.Parse(eng.Attribute("Cost")!.Value));
        return returnedEngineer;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        XElement xmlEngineers = XMLTools.LoadListFromXMLElement("engineers");
        if (filter is null)
            filter = (e) => true;
        List<Engineer> engineersList = 
            xmlEngineers.Descendants("Engineer")
            .Select(engin => {
            Engineer engineer_t = new(
                                        int.Parse(engin.Attribute("Id")!.Value),
                                        engin.Value,
                                        engin.Attribute("Email")!.Value,
                                        (EngineerExperience)Enum.Parse(typeof(EngineerExperience), engin.Attribute("Level")!.Value),
                                        double.Parse(engin.Attribute("Cost")!.Value)
                                                );
                         return engineer_t; })
            .Where(engin =>  filter(engin) )
            .ToList();
        return engineersList;
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
