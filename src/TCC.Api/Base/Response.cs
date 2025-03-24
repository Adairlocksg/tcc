using System.Net;

namespace TCC.Api.Base
{
    public class Response
    {
        public Response(HttpStatusCode statusCode, bool success, string message, object content = null)
        {
            StatusCode = (int)statusCode;
            Success = success;
            Message = message;
            Content = content;
        }

        public Response(HttpStatusCode statusCode, bool success, List<string> messages, object content = null)
        {
            StatusCode = (int)statusCode;
            Success = success;
            Message = string.Join("</br>", messages);
            Content = content;
        }

        public int StatusCode { get; }
        public object Content { get; }
        public bool Success { get; }
        public string Message { get; }
    }
}
