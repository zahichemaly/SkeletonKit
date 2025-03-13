namespace SkeletonKit.Storage.Abstractions
{
    public interface IAttachmentProvider
    {
        Task<byte[]> Get(string relativePath, bool ignoreMicrosericeName = false);
        Task<byte[]> GetFromFullPath(string fullPath);
        Task<string> GetText(string relativePath);
        Task<IEnumerable<string>> GetAll(string name);
        Task Write(string filename, byte[] file);
        Task<bool> Exist(string relativePath, bool ignoreMicrosericeName = false);
        Task Delete(string relativePath);
    }
}
