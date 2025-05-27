using System.Text.Json;

namespace Mail.Engine.Service.Function.Helpers
{
    public class RequestBodyExtractorHelper
    {
        // Extract a specific field like BillMonth
        public static async Task<T> ExtractRequestModelAsync<T>(Stream body) where T : new()
        {
            // Extract the entire request body
            var data = await ExtractAsync(body);

            // Deserialize the request body into the type T; return a new instance of T if the body is empty
            return data.ToString() == "" ? new T() : JsonSerializer.Deserialize<T>(data.GetRawText())!;
        }

        private static async Task<JsonElement> ExtractAsync(Stream body)
        {
            // Read the request body
            string requestBody = await new StreamReader(body).ReadToEndAsync();

            // Deserialize into JsonElement; return an empty JsonElement if request body is empty
            return string.IsNullOrEmpty(requestBody)
                ? new JsonElement()
                : JsonSerializer.Deserialize<JsonElement>(requestBody)!;
        }
    }
}
