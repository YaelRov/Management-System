

using DalApi;
using DO;
using System.Reflection;
using System.Text;
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
        /// <summary>
        /// checking if two lists are equal
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(List<int?>? x, List<int?>? y)
        {
            if (x is not null && y is not null)
                return x.SequenceEqual(y);
            if (x is null && y is null)
                return true;
            return false;
        }

        /// <summary>
        /// calculate sum of elements in list
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>sum of elements in list</returns>
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
    #region writing and getting from the xml files
    const string s_xml_dir = @"..\xml\";
    /// <summary>
    /// Load List From XMLElement
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>XElement</returns>
    /// <exception cref="DalXMLFileLoadCreateException"></exception>
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
    /// <summary>
    /// Save List To XMLElement
    /// </summary>
    /// <param name="rootElem">type XElement</param>
    /// <param name="entity">type string</param>
    /// <exception cref="DalXMLFileLoadCreateException"></exception>    
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
    #endregion

    /// <summary>
    /// To String Property 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns>string</returns>
    public static string ToStringProperty<T>(this T obj)
    {
        StringBuilder sb = new StringBuilder();
        Type objType = obj.GetType();
        PropertyInfo[] properties = objType.GetProperties();

        foreach (PropertyInfo property in properties)
        {
            object? propertyValue = property.GetValue(obj);

            if (propertyValue == null || (propertyValue is IEnumerable<object> collection && !collection.Any()))
                continue;

            sb.Append($"{property.Name}: ");

            if (typeof(IEnumerable<T>).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
            {
                IEnumerable<object> collection1 = (IEnumerable<object>)propertyValue;
                sb.Append("[ ");

                foreach (var item in collection1)
                {
                    sb.Append(item.ToString() + ", ");
                }

                if (sb.Length > 2)
                {
                    sb.Length -= 2;
                }

                sb.Append(" ]");
            }
            else
            {
                sb.Append(propertyValue.ToString());
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}
