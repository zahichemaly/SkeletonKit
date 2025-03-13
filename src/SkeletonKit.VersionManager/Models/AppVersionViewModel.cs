namespace SkeletonKit.VersionManager.Models
{
    /// <summary>
    /// This class represents the view model of the app version
    /// </summary>
    public class AppVersionViewModel
    {
        public int Id { get; set; }
        public string OsName { get; set; }
        public bool UseMinimumSupportedVersion { get; set; }
        public string LatestVersion { get; set; }
        public string MinimumSupportedVersion { get; set; }
        public string ExceptionsVersions { get; set; }
        public string UnsupportedVersions { get; set; }
    }
}
