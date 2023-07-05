namespace scada_back.Exception;

public class ObjectNameAlreadyExists:  System.Exception
{
    public ObjectNameAlreadyExists()
    {
    }

    public ObjectNameAlreadyExists(string? message) : base(message)
    {
    }

    public ObjectNameAlreadyExists(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}