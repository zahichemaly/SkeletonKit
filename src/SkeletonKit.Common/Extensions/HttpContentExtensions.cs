using Newtonsoft.Json;

namespace CME.Common.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            try
            {
                var jsonString = await content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch
            {
                return default;
            }
        }
    }
}
