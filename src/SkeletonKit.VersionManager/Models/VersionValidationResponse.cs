namespace SkeletonKit.VersionManager.Models
{
    internal class VersionValidationResponse
    {
        public Error Error { get; set; }
        public int StatusCode { get; set; }
        public DateTime? MaintenanceTime { get; set; }
        public bool IsSupported
        {
            get
            {
                return Error == null;
            }
        }
        public bool IsUnavailable
        {
            get
            {
                return MaintenanceTime != null;
            }
        }
        public string MaintenanceDisplayTime
        {
            get
            {
                var time = MaintenanceTime ?? DateTime.UtcNow;
                DateTime cstTime = time.ToUniversalTime();
                return cstTime.ToString("ddd, d MMM yyyy HH:mm:ss \"GMT\"");
            }
        }
    }
}
