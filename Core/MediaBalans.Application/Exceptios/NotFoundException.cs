
namespace MediaBalans.Application.Exceptios
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : this("No records found")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(Exception ex) : this(ex.Message)
        {

        }

    }
}
