namespace scada_back.Exception;

public class ActionNotExecutedException : System.Exception
{
    public ActionNotExecutedException()
    {
    }

    public ActionNotExecutedException(string? message) : base(message)
    {
    }

    public ActionNotExecutedException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    } 
}