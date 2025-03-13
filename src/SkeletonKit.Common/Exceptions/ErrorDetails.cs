using Newtonsoft.Json;

namespace CME.Common.Exceptions
{
    public class ErrorDetails
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
