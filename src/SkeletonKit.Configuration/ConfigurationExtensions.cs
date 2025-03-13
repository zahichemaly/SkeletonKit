using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace SkeletonKit.Configuration
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Builds an <seealso cref="IConfig"/> instance based on environment variables that match the <seealso cref="EnvironmentVariableAttribute"/>
        /// on each propery of the class if any. Otherwise, the values will be populated based on the AppSettings.json through <seealso cref="IConfiguration"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <returns>The <seealso cref="IConfig"/> instance.</returns>
        public static T GetConfig<T>(this IConfiguration configuration, string sectionName = null) where T : IConfig
        {
            var name = sectionName ?? typeof(T).Name;
            var sectionConfig = configuration.GetSection(name).Get<T>() ?? Activator.CreateInstance<T>();

            foreach (var property in typeof(T).GetProperties())
            {
                var envAttribute = property.GetCustomAttribute<EnvironmentVariableAttribute>();
                if (envAttribute != null)
                {
                    string envValue = Environment.GetEnvironmentVariable(envAttribute.VariableName);
                    if (!string.IsNullOrWhiteSpace(envValue))
                    {
                        property.SetValue(sectionConfig, Convert.ChangeType(envValue, property.PropertyType));
                    }
                    else
                    {
                        property.SetValue(sectionConfig, property.GetValue(sectionConfig));
                    }
                }
            }

            return sectionConfig;
        }
    }
}
