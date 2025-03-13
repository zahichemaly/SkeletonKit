namespace SkeletonKit.Security
{
    public interface ICryptographyHelper
    {
        string Encrypt(string text);
        string Decrypt(string encrypted);
    }
}
