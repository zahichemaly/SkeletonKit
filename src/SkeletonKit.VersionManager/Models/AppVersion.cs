namespace SkeletonKit.VersionManager.Models
{
    /// <summary>
    /// This class represents the model of the table AppVersion
    /// </summary>
    public class AppVersion
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public string OsName { get; set; }
        public bool UseMinimumSupportedVersion { get; set; }
        public string MinimumSupportedVersion { get; set; }
        public string LatestVersion { get; set; }
        public string ExceptionsVersions { get; set; }
        public string UnsupportedVersions { get; set; }
        public DateTime? MaintenanceTime { get; set; }
    }
}
