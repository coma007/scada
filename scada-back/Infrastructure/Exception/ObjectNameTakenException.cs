namespace scada_back.Infrastructure.Exception;

public class ObjectNameTakenException:  System.Exception
{
    public ObjectNameTakenException()
    {
    }

    public ObjectNameTakenException(string? message) : base(message)
    {
    }

    public ObjectNameTakenException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}