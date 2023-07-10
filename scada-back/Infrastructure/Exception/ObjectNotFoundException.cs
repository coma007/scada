namespace scada_back.Infrastructure.Exception;

public class ObjectNotFoundException : System.Exception
{
    public ObjectNotFoundException()
    {
    }

    public ObjectNotFoundException(string? message) : base(message)
    {
    }

    public ObjectNotFoundException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}