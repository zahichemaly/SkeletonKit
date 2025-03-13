
using SkeletonKit.Configuration;

namespace SkeletonKit.Storage.Configurations
{
    public class StorageSettings : IConfig
    {
        [EnvironmentVariable("STORAGE_TYPE")]
        public string Type { get; set; }

        #region Azure
        [EnvironmentVariable("STORAGE_ACCOUNT_URL")]
        public string AccountUrl { get; set; }
        [EnvironmentVariable("STORAGE_ACCOUNT_NAME")]
        public string AccountName { get; set; }
        [EnvironmentVariable("STORAGE_ACCOUNT_KEY")]
        public string AccountKey { get; set; }
        [EnvironmentVariable("STORAGE_CONTAINER_NAME")]
        public string ContainerName { get; set; }
        #endregion

        #region Local
        [EnvironmentVariable("STORAGE_FILE_PATH")]
        public string FilePath { get; set; }
        #endregion

        #region FTP
        [EnvironmentVariable("STORAGE_FTP_HOST")]
        public string FtpHost { get; set; }
        [EnvironmentVariable("STORAGE_FTP_USERNAME")]
        public string FtpUsername { get; set; }
        [EnvironmentVariable("STORAGE_FTP_PASSWORD")]
        public string FtpPassword { get; set; }
        #endregion
    }
}
