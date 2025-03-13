using CacheManager.Core;
using SkeletonKit.MultiTenancy.Abstractions.Providers;
using SkeletonKit.Storage.Abstractions;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;

namespace SkeletonKit.Security
{
    internal class RSACryptographyHelper : ICryptographyHelper
    {
        private readonly RSACryptoServiceProvider _privateKey;
        private readonly RSACryptoServiceProvider _publicKey;
        private readonly string _cacheRegion;
        private readonly IAttachmentProvider _attachmentProvider;
        private readonly ICacheManager<string> _cacheManager;
        private readonly CryptographySettings _cryptographySettings;

        public RSACryptographyHelper(CryptographySettings cryptographySettings,
            IAttachmentProvider attachmentProvider,
            ICacheManager<string> cacheManager,
            ITenantProvider tenantProvider)
        {
            _attachmentProvider = attachmentProvider;
            _cacheManager = cacheManager;
            _cryptographySettings = cryptographySettings;

            var tenantId = tenantProvider.GetTenantId();
            _cacheRegion = tenantId;
            _publicKey = GetPublicKeyFromPemFile($"/{tenantId}private/keys/{cryptographySettings.PrivateKeyName}");
            _privateKey = GetPrivateKeyFromPemFile($"/{tenantId}private/keys/{cryptographySettings.PrivateKeyName}");
        }

        public string Encrypt(string text)
        {
            var encryptedBytes = _publicKey.Encrypt(Encoding.UTF8.GetBytes(text), false);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string encrypted)
        {
            var decryptedBytes = _privateKey.Decrypt(Convert.FromBase64String(encrypted), false);
            return Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
        }

        private RSACryptoServiceProvider GetPrivateKeyFromPemFile(string filePath)
        {
            string key = "private";
            string privatekey = _cacheManager.Get(key, _cacheRegion);
            if (privatekey == null)
            {
                var task = Task.Run(async () => await _attachmentProvider.GetText(filePath));
                privatekey = task.Result;
                _cacheManager.Add(key, privatekey, _cacheRegion);
            }

            using (TextReader privateKeyTextReader = new StringReader(privatekey))
            {
                AsymmetricCipherKeyPair readKeyPair = (AsymmetricCipherKeyPair)new PemReader(privateKeyTextReader, new PasswordFinder(_cryptographySettings.Password)).ReadObject();
                RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)readKeyPair.Private);
                RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
                csp.ImportParameters(rsaParams);
                return csp;
            }
        }

        private RSACryptoServiceProvider GetPublicKeyFromPemFile(string filePath)
        {
            string key = "public";
            string publickey = _cacheManager.Get(key, _cacheRegion);
            if (publickey == null)
            {
                var task = Task.Run(async () => await _attachmentProvider.GetText(filePath));
                publickey = task.Result;
                _cacheManager.Add(key, publickey, _cacheRegion);
            }

            using (TextReader publicKeyTextReader = new StringReader(publickey))
            {
                RsaKeyParameters publicKeyParam = (RsaKeyParameters)new PemReader(publicKeyTextReader, new PasswordFinder(_cryptographySettings.Password)).ReadObject();

                RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaKeyParameters)publicKeyParam);

                RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
                csp.ImportParameters(rsaParams);
                return csp;
            }
        }
    }
}
