namespace SkeletonKit.VersionManager
{
    /// <summary>
    /// This class provides configurations related to the dashboard and the responses of the API like error responses.
    /// </summary>
    internal class Constants
    {
        public const string OsNameHeaderKey = "X-Os-Name";
        public const string AppVersionHeaderKey = "X-App-Version";

        public const string AppVersionHeaderMissingCode = "app_version_header_required";
        public const string AppVersionHeaderMissingMessage = "The App Version Header is required";

        public const string OsNameHeaderMissingCode = "os_name_header_required";
        public const string OsNameHeaderMissingMessage = "The OS Name Header is required";

        public const string ForceUpdateCode = "force_update";
        public const string ForceUpdateMessage = "Your application version is no longer supported! Check your store for update.";
        public const int ForceUpdateHttpStatusCode = 426; // 426 Upgrade Required client error response code indicates that the server refuses to perform the request using the current protocol but might be willing to do so after the client upgrades to a different protocol.

        public const string MaintenanceCode = "service_unavailable";
        public const string MaintenanceMessage = "Server is currently down for maintenance.";
        public const int MaintenanceHttpStatusCode = 503; // 503 Service Unavailable server error response code indicates that the server is not ready to handle the request.
    }
}
