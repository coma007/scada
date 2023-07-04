namespace scada_back.Exception;

public class ObjectNotFound : System.Exception
{
    public ObjectNotFound()
    {
    }

    public ObjectNotFound(string? message) : base(message)
    {
    }

    public ObjectNotFound(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}