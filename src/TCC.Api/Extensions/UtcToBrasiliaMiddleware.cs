using System.Text.Json;
using System.Text.Json.Nodes;

namespace TCC.Api.Extensions
{
    public class UtcToBrasiliaMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        private readonly TimeZoneInfo _brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context); // Deixa o pipeline continuar (chama o Controller, etc)

            if (context.Response.ContentType != null && context.Response.ContentType.Contains("application/json"))
            {
                memoryStream.Seek(0, SeekOrigin.Begin);

                var json = await JsonNode.ParseAsync(memoryStream);
                if (json != null)
                {
                    ConvertUtcDates(json);

                    context.Response.Body = originalBodyStream;
                    context.Response.ContentLength = null; // Reset para regravar
                    await context.Response.WriteAsync(json.ToJsonString(new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = false
                    }));
                }
            }
            else
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                await memoryStream.CopyToAsync(originalBodyStream);
            }
        }

        private void ConvertUtcDates(JsonNode node)
        {
            if (node is JsonObject jsonObject)
            {
                foreach (var prop in jsonObject)
                {
                    ConvertUtcDates(prop.Value);
                }
            }
            else if (node is JsonArray jsonArray)
            {
                foreach (var item in jsonArray)
                {
                    ConvertUtcDates(item);
                }
            }
            else if (node is JsonValue value && value.TryGetValue<DateTime>(out var dateTimeValue))
            {
                if (dateTimeValue.Kind == DateTimeKind.Utc)
                {
                    var converted = TimeZoneInfo.ConvertTimeFromUtc(dateTimeValue, _brazilTimeZone);
                    value.ReplaceWith(converted);
                }
            }
        }
    }
}
