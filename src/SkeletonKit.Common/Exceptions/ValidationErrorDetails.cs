using Newtonsoft.Json;

namespace SkeletonKit.Common.Exceptions
{
    public sealed class ValidationErrorDetails : ErrorDetails
    {
        [JsonProperty("errors")]
        public IEnumerable<ErrorDetails> Errors { get; set; } = new List<ErrorDetails>();
    }
}
