using Org.BouncyCastle.OpenSsl;

namespace SkeletonKit.Security
{
    internal class PasswordFinder : IPasswordFinder
    {
        private string password;

        public PasswordFinder(string password)
        {
            this.password = password;
        }


        public char[] GetPassword()
        {
            return password.ToCharArray();
        }
    }
}
