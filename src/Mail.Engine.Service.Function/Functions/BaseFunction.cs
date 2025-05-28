using Mail.Engine.Service.Function.Helpers;

namespace Mail.Engine.Service.Function.Functions
{
    public class BaseFunction<T> where T : class, new()
    {
        public static async Task<T> RequestBody(Stream body)
        {
            return await RequestBodyExtractorHelper.ExtractRequestModelAsync<T>(body);
        }
    }
}
