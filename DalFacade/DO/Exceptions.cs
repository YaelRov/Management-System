

namespace DO;
[Serializable]
public class DalDoesNotExistException : Exception
{
    /// <summary>
    /// a c-tor of exception that occures when something does not exists
    /// </summary>
    /// <param name="message">nullable atring</param>
    public DalDoesNotExistException(string? message) : base(message) { }
}
[Serializable]
public class DalAlreadyExistsException : Exception
{
    /// <summary>
    /// a c-tor of exception that occures when something is already not exists
    /// </summary>
    /// <param name="message">nullable atring</param>
    public DalAlreadyExistsException(string? message) : base(message) { }
}
[Serializable]
public class DalXMLFileLoadCreateException : Exception
{
    /// <summary>
    /// a c-tor of exception that occures when it's a promlem with XML file
    /// </summary>
    /// <param name="message">nullable atring</param>
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}