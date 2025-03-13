namespace SkeletonKit.Storage.Abstractions
{
    public interface IAttachmentDirectoryProvider
    {
        string GetDirectoryPath(string fileRelativePath);
        string GetFullPath(string fileName);
    }
}
