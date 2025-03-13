using CME.MultiTenancy.Abstractions.Providers;
using CME.Storage.Abstractions;
using CME.Storage.Configurations;

namespace CME.Storage
{
    internal class AttachmentDirectoryProvider : IAttachmentDirectoryProvider
    {
        private readonly ITenantProvider _tenantProvider;
        private readonly StorageSettings _storageSettings;

        public AttachmentDirectoryProvider(ITenantProvider tenantProvider, StorageSettings storageSettings)
        {
            _tenantProvider = tenantProvider;
            _storageSettings = storageSettings;
        }

        private string DirectoryPath
        {
            get
            {
                return _tenantProvider.GetTenantId().ToLowerInvariant();
            }
        }

        public string GetDirectoryPath(string fileRelativePath)
        {
            string _directoryPath = string.Concat(DirectoryPath, "/", fileRelativePath);
            return _directoryPath;
        }

        public string GetFullPath(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return null;
            switch (_storageSettings.Type.ToLowerInvariant())
            {
                case Constants.StorageType.Azure:
                    return string.Concat(_storageSettings.AccountUrl, $"/{GetDirectoryPath(fileName)}");
                case Constants.StorageType.Local:
                    var path = _storageSettings.FilePath;
                    path = path.Replace('\\', '/');
                    return string.Concat(path, $"/{GetDirectoryPath(fileName)}");
                default:
                    return null;
            }
        }
    }
}
