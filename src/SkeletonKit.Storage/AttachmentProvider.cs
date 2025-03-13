using SkeletonKit.Storage.Abstractions;
using SkeletonKit.Storage.Configurations;
using Storage.Net.Blobs;

namespace SkeletonKit.Storage
{
    internal class AttachmentProvider : IAttachmentProvider
    {
        private readonly StorageSettings _storageSettings;
        private readonly IAttachmentDirectoryProvider _attachmentDirectoryProvider;
        private readonly BlobStorageFactory _blobStorageFactory;

        public AttachmentProvider(StorageSettings storageSettings,
            IAttachmentDirectoryProvider attachmentDirectoryProvider,
            BlobStorageFactory blobStorageFactory)
        {
            _storageSettings = storageSettings;
            _attachmentDirectoryProvider = attachmentDirectoryProvider;
            _blobStorageFactory = blobStorageFactory;
        }

        public IBlobStorage GetblobStorage()
        {
            return _blobStorageFactory.GetBlobStorage();
        }

        public async Task<byte[]> Get(string relativePath, bool ignoreMicrosericeName = false)
        {
            byte[] bytes = null;

            using (var bs = GetblobStorage())
            {
                relativePath = GetFullPath(relativePath, ignoreMicrosericeName);
                bool isExist = await bs.ExistsAsync(relativePath);

                if (!isExist)
                {
                    return bytes;
                }

                bytes = await bs.ReadBytesAsync(relativePath);
                return bytes;
            }
        }

        public async Task<IEnumerable<string>> GetAll(string name)
        {
            string folderPath = GetFullPath(name);
            using (var bs = GetblobStorage())
            {
                var blob = await bs.ListFilesAsync(new ListOptions() { FolderPath = folderPath });
                IEnumerable<string> files = null;
                if (blob != null)
                {
                    files = blob.Select(x => x.Name);
                }
                return files;
            }
        }

        public async Task Write(string filename, byte[] file)
        {
            string filePath = GetFullPath(filename);
            using (var bs = GetblobStorage())
            {
                await bs.WriteAsync(filePath, file);
            }
        }

        public async Task<bool> Exist(string relativePath, bool ignoreMicrosericeName = false)
        {
            string filePath = GetFullPath(relativePath, ignoreMicrosericeName);
            using (var bs = GetblobStorage())
            {
                return await bs.ExistsAsync(filePath);
            }
        }

        public async Task Delete(string relativePath)
        {
            string filePath = GetFullPath(relativePath);
            using (var bs = GetblobStorage())
            {
                await bs.DeleteAsync(filePath);
            }
        }

        private string GetFullPath(string relativePath, bool ignoreMicrosericeName = false)
        {
            string filePath = !ignoreMicrosericeName ? _attachmentDirectoryProvider.GetDirectoryPath(relativePath) : relativePath;
            if (!string.IsNullOrEmpty(_storageSettings.ContainerName))
            {
                return string.Concat(_storageSettings.ContainerName, "/", filePath); ;
            }
            return filePath;
        }

        public async Task<byte[]> GetFromFullPath(string fullPath)
        {
            byte[] bytes = null;

            using (var bs = GetblobStorage())
            {
                bool isExist = await bs.ExistsAsync(fullPath);

                if (!isExist)
                {
                    return bytes;
                }

                bytes = await bs.ReadBytesAsync(fullPath);
                return bytes;
            }
        }

        public async Task<string> GetText(string relativePath)
        {
            string bytes = null;

            using (var bs = GetblobStorage())
            {
                bool isExist = await bs.ExistsAsync(relativePath);

                if (!isExist)
                {
                    return bytes;
                }

                bytes = await bs.ReadTextAsync(relativePath);
                return bytes;
            }
        }
    }
}
