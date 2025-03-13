namespace SkeletonKit.Configuration.Settings
{
    /// <summary>
    /// Sample config
    /// </summary>
    internal sealed class TestSettings : IConfig
    {
        [EnvironmentVariable("CONFIG_1")]
        public string Config1 { get; set; }

        [EnvironmentVariable("CONFIG_2")]
        public string Config2 { get; set; }
    }
}
