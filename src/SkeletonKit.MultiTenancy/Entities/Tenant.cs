namespace SkeletonKit.MultiTenancy.Entities
{
    public class Tenant
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public bool RequireEmailActivation { get; set; }
        public bool RequireTwoFactorAuthentication { get; set; }
        public bool RequireMobileNumberActivation { get; set; }

        public bool LockoutEnabled { get; set; }

        public int MaximumNumberOfEmailAttempts { get; set; }
        public int EmailThreshold { get; set; }

        public int MaximumNumberOfOTPAttempts { get; set; }
        public int OTPActivationThreshold { get; set; }
    }
}
