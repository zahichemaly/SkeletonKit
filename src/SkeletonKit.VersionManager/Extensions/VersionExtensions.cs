namespace CME.VersionManager.Extensions
{
    internal static class VersionExtensions
    {
        /// <summary>
        /// This extension method is to check whether the version exists in the given list or not
        /// </summary>
        /// <param name="version">the version to check</param>
        /// <param name="versions">The version list to search in</param>
        /// <returns>Returns a boolean indicating whether the version exists or not</returns>
        public static bool Exists(this Version version, List<Version> versions)
        {
            foreach (Version ver in versions)
            {
                if (version == ver)
                    return true;
            }
            return false;
        }
    }
}
