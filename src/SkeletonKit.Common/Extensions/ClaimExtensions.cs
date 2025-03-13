using Newtonsoft.Json;
using System.Security.Claims;

namespace SkeletonKit.Common.Extensions
{
    public static class ClaimExtensions
    {
        public static Claim Get(this IEnumerable<Claim> claims, string type)
        {
            Claim claim = claims.FirstOrDefault(x => x.Type.Equals(type));
            return claim;
        }
        public static T GetValue<T>(this IEnumerable<Claim> claims, string type) where T : class
        {
            Claim claim = Get(claims, type);
            if (claim != null)
            {
                string value = string.Empty;
                if (typeof(T).FullName.Equals(typeof(Lookup).FullName))
                {
                    Lookup lookup = JsonConvert.DeserializeObject<Lookup>(claim.Value);
                    return lookup as T;
                }
                else
                {
                    return (T)Convert.ChangeType(claim.Value, typeof(T));
                }
            }
            return default(T);
        }
    }

    internal class Lookup
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
