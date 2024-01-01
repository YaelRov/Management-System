
namespace BO;
[Serializable]
public class BlDoesNotExistException : Exception
{
    /// <summary>
    /// a c-tor of exception that occures when something does not exists
    /// </summary>
    /// <param name="message">nullable atring</param>
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
: base(message, innerException) { }
}
[Serializable]
public class BlAlreadyExistsException : Exception
{
    /// <summary>
    /// a c-tor of exception that occures when something is already not exists
    /// </summary>
    /// <param name="message">nullable atring</param>
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
: base(message, innerException) { }

}
[Serializable]
public class BlXMLFileLoadCreateException : Exception
{
    /// <summary>
    /// a c-tor of exception that occures when it's a promlem with XML file
    /// </summary>
    /// <param name="message">nullable atring</param>
    public BlXMLFileLoadCreateException(string? message) : base(message) { }
    public BlXMLFileLoadCreateException(string message, Exception innerException)
: base(message, innerException) { }

}

[Serializable]
public class BlNullPropertyException : Exception
{
    /// <summary>
    /// a c-tor of exception that occures when it's a null property
    /// </summary>
    /// <param name="message">nullable atring</param>
    public BlNullPropertyException(string? message) : base(message) { }
}


[Serializable]
public class BlInsufficientTime: Exception
{
    /// <summary>
    /// a c-tor of exception that occures when it's a insufficient time
    /// </summary>
    /// <param name="message">nullable atring</param>
    public BlInsufficientTime(string? message) : base(message) { }
}


[Serializable]
public class BlInvalidInput : Exception
{
    /// <summary>
    /// a c-tor of exception that occures when it's an invalid input
    /// </summary>
    /// <param name="message">nullable atring</param>
    public BlInvalidInput(string? message) : base(message) { }
}