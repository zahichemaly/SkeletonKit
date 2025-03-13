namespace SkeletonKit.VersionManager.Exceptions
{
    /// <summary>
    /// This exception is to be thrown when the version sent has invalid format
    /// </summary>
    internal class VersionFormatException : VersionManagerBaseException
    {
        private static string VersionFormatExceptionCode = "invalid_version_format";
        private static string VersionFormatExceptionMessage = "Check the format of the version that you are sending!";

        public VersionFormatException() : base(VersionFormatExceptionCode, VersionFormatExceptionMessage) { }
    }
}
