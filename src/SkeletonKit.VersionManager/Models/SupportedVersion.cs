namespace CME.VersionManager.Models
{
    /// <summary>
    /// This class represents a model for the response of the call that checks if a version is supported or not
    /// </summary>
    public class SupportedVersion
    {
        /// <summary>
        /// This property shows a boolean indicating whether the version is supported or not
        /// </summary>
        public bool Supported { get; set; }
        /// <summary>
        /// This property shows the latest available version
        /// </summary>
        public string LatestVersion { get; set; }
        /// <summary>
        /// This property shows a date indicating the expected maintenance end date
        /// </summary>
        public DateTime? MaintenanceTime { get; set; }
    }
}
