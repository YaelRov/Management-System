

using DalApi;
using DO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BO;

public static class Tools
{
    /// <summary>
    /// A class that implementats IEqualityComparer for List<int?>
    /// </summary>
    public class DistinctIntList : IEqualityComparer<List<int?>>
    {
        public bool Equals(List<int?>? x, List<int?>? y)
        {
            if (x is not null && y is not null)
                return x.SequenceEqual(y);
            if (x is null && y is null)
                return true;
            return false;
        }

        //Return sum of elements in list
        public int GetHashCode(List<int?> obj)
        {
            int sum = 0;
            foreach (int? num in obj)
            {
                sum += num ?? default(int);
            }
            return sum;
        }
    }

    const string s_xml_dir = @"..\xml\";

    public static XElement LoadListFromXMLElement(string entity)
    {
        string filePath = $"{s_xml_dir}{entity}.xml";
        try
        {
            if (File.Exists(filePath))
                return XElement.Load(filePath);
            XElement rootElem = new(entity);
            rootElem.Save(filePath);
            return rootElem;
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }

    public static void SaveListToXMLElement(XElement rootElem, string entity)
    {
        string filePath = $"{s_xml_dir}{entity}.xml";
        try
        {
            rootElem.Save(filePath);
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }

}
