
namespace MediaBalans.Application.Exceptios
{
    public class ValidationException : Exception
    {
        public ValidationException() : this("Validation error onccured")
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(Exception ex) : this(ex.Message)
        {

        }

    }
}
