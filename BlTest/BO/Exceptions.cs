
namespace BlTest.BO;

[Serializable]
public class BlDoesNotExistException : Exception
{
    /// <summary>
    /// a c-tor of exception that occures when something does not exists
    /// </summary>
    /// <param name="message">nullable atring</param>
    public BlDoesNotExistException(string? message) : base(message) { }
}
[Serializable]
public class BlAlreadyExistsException : Exception
{
    /// <summary>
    /// a c-tor of exception that occures when something is already not exists
    /// </summary>
    /// <param name="message">nullable atring</param>
    public BlAlreadyExistsException(string? message) : base(message) { }
}
[Serializable]
public class BlXMLFileLoadCreateException : Exception
{
    /// <summary>
    /// a c-tor of exception that occures when it's a promlem with XML file
    /// </summary>
    /// <param name="message">nullable atring</param>
    public BlXMLFileLoadCreateException(string? message) : base(message) { }
}
