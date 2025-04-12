using System.Net;

namespace TCC.Api.Base
{
    public class Response
    {
        public Response(string statusCode, bool success, string message, object content = null)
        {
            StatusCode = statusCode;
            Success = success;
            Message = message;
            Content = content;
        }

        public Response(string statusCode, bool success, List<string> messages, object content = null)
        {
            StatusCode = statusCode;
            Success = success;
            Message = string.Join("</br>", messages);
            Content = content;
        }

        public string StatusCode { get; }
        public object Content { get; }
        public bool Success { get; }
        public string Message { get; }
    }
}
