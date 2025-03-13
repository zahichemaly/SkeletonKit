using CME.Storage.Configurations;
using Storage.Net;
using Storage.Net.Blobs;
using System.Net;

namespace CME.Storage
{
    /// <summary>
    /// Factory used to initialize the storage, based on the Clients appseting 
    /// it will either load Azure storage, Local or FTP storage.
    /// </summary>
    internal class BlobStorageFactory
    {
        public readonly StorageSettings _storageSettings;

        public BlobStorageFactory(StorageSettings storageSettings)
        {
            _storageSettings = storageSettings;
        }

        public IBlobStorage GetBlobStorage()
        {
            IBlobStorage blobStorage = null;
            switch (_storageSettings.Type.ToLowerInvariant())
            {
                case Constants.StorageType.Azure:
                    blobStorage = StorageFactory.Blobs.AzureBlobStorage(_storageSettings.AccountKey, _storageSettings.AccountName);
                    break;
                case Constants.StorageType.Local:
                    blobStorage = StorageFactory.Blobs.DirectoryFiles(_storageSettings.FilePath);
                    break;
                case Constants.StorageType.Ftp:
                    blobStorage = StorageFactory.Blobs.Ftp(_storageSettings.FtpHost, new NetworkCredential()
                    {
                        UserName = _storageSettings.FtpUsername,
                        Password = _storageSettings.FtpPassword,
                    });
                    break;
                default:
                    throw new InvalidOperationException("Invalid storage type in configuration.");
            }
            return blobStorage;
        }
    }
}
