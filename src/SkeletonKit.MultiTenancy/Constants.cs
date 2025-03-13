namespace SkeletonKit.MultiTenancy
{
    public class Constants
    {
        public static class Headers
        {
            public const string TenantId = "X-TenantId";
            public const string TenantConfig = "X-TenantConfig";
        }

        public static class QueryParams
        {
            public const string Tenant = "tenant";
        }
    }
}
