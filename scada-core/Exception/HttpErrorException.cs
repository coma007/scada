namespace scada_core.Exception
{
    public class HttpErrorException : System.Exception
    {
        public HttpErrorException()
        {
        }

        public HttpErrorException(string? message) : base(message)
        {
        }

        public HttpErrorException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}