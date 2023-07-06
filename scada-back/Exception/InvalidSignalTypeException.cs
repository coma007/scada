namespace scada_back.Exception;

public class InvalidSignalTypeException: System.Exception
{
    public InvalidSignalTypeException()
    {
    }

    public InvalidSignalTypeException(string? message) : base(message)
    {
    }

    public InvalidSignalTypeException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    } 
}