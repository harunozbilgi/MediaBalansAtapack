
namespace MediaBalans.Application.Wrappers
{
    public class Response<T>
    {
        public T Data { get; private set; }
        public string Messages { get; private set; }
        public int StatusCode { get; private set; }
        public bool IsSuccessful { get; private set; }
        public List<string> Errors { get; private set; }
        public static Response<T> Success(T data)
        {
            return new Response<T> { Data = data, Messages = default, StatusCode = default, IsSuccessful = true };
        }
        public static Response<T> Success(T data, string messages, int statusCode)
        {
            return new Response<T> { Data = data, Messages = messages, StatusCode = statusCode, IsSuccessful = true };
        }
        public static Response<T> Success(string messages, int statusCode)
        {
            return new Response<T> { Data = default, Messages = messages, StatusCode = statusCode, IsSuccessful = true };
        }
        public static Response<T> Error(List<string> errors, int statusCode)
        {
            return new Response<T> { Data = default, Errors = errors, Messages = default, StatusCode = statusCode, IsSuccessful = false };
        }
        public static Response<T> Error(string error, int statusCode)
        {
            return new Response<T> { Data = default, Errors = new List<string>() { error }, Messages = default, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
