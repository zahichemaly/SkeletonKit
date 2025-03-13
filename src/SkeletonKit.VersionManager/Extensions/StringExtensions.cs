using SkeletonKit.VersionManager.Models;

namespace SkeletonKit.VersionManager.Extensions
{
    /// <summary>
    /// This class is a static class that provides extension methods
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// This extension method is to validate the format of the Json holding the versions
        /// </summary>
        /// <param name="jsonStr">The json to validate</param>
        /// <returns>Returns a boolean indicating whether the json format is valid or not</returns>
        public static bool IsValidVersionsJsonFormat(this string jsonStr)
        {
            if (string.IsNullOrWhiteSpace(jsonStr))
                return false;
            try
            {
                VersionsJsonModel versionsJsonModel = Newtonsoft.Json.JsonConvert.DeserializeObject<VersionsJsonModel>(jsonStr);
                if (versionsJsonModel.Versions?.Count > 0)
                {
                    Version version = null;
                    foreach (string versionStr in versionsJsonModel.Versions)
                    {
                        if (!Version.TryParse(versionStr, out version))
                            return false;
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// This extension method is to convert the json string containing the versions into a list of Version model
        /// </summary>
        /// <param name="jsonStr">The json string to convert</param>
        /// <returns>Returns the list of Version model</returns>
        public static List<Version> AsVersionList(this string jsonStr)
        {
            List<Version> versionList = new List<Version>();
            if (!string.IsNullOrWhiteSpace(jsonStr))
            {
                VersionsJsonModel versionsJsonModel = Newtonsoft.Json.JsonConvert.DeserializeObject<VersionsJsonModel>(jsonStr);
                if (versionsJsonModel.Versions?.Count > 0)
                {
                    foreach (string versionStr in versionsJsonModel.Versions)
                    {
                        Version version = null;
                        Version.TryParse(versionStr, out version);
                        versionList.Add(version);
                    }
                }
            }
            return versionList;
        }

        /// <summary>
        /// This extension method is to check whether the version string has a valid format or not
        /// </summary>
        /// <param name="versionStr">the version string to check</param>
        /// <returns>Returns a boolean indicating whether the version string is valid or not</returns>
        public static bool IsValidVersionFormat(this string versionStr)
        {
            Version version = null;
            return Version.TryParse(versionStr, out version);
        }
    }
}
