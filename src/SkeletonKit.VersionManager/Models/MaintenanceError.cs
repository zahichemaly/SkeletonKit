using Newtonsoft.Json;

namespace SkeletonKit.VersionManager.Models
{
    internal class MaintenanceError : Error
    {
        public MaintenanceError(DateTime? date)
        {
            Code = Constants.MaintenanceCode;
            Message = Constants.MaintenanceMessage;
            ServerUpExpectedDate = date?.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        [JsonProperty("serverUpExpectedDate")]
        public string ServerUpExpectedDate { get; set; }
    }
}
