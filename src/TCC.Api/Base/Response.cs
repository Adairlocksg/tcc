using System.Net;

namespace TCC.Api.Base
{
    public class Response(HttpStatusCode statusCode, bool success, List<string> messages, object content = null)
    {
        public int StatusCode { get; } = (int)statusCode;
        public object Content { get; } = content;
        public bool Success { get; } = success;
        public string Message { get; } = string.Join("</br>", messages);
    }
}
