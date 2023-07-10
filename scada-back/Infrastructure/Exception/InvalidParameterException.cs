namespace scada_back.Infrastructure.Exception;

public class InvalidParameterException : System.Exception
{
    public InvalidParameterException()
    {
    }

    public InvalidParameterException(string? message) : base(message)
    {
    }

    public InvalidParameterException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    } 
}