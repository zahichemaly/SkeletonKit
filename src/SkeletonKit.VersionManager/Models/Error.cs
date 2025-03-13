using Newtonsoft.Json;

namespace CME.VersionManager.Models
{
    /// <summary>
    /// This class represents an error model
    /// </summary>
    internal class Error
    {
        /// <summary>
        /// This property shows the error code
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        /// This property shows the error description
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
