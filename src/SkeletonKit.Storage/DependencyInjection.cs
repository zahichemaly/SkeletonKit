using CME.Configuration;
using CME.Storage.Abstractions;
using CME.Storage.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CME.Storage
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStorage(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var storageSettings = configuration.GetConfig<StorageSettings>();
            services.AddStorage(storageSettings);
            return services;
        }

        public static IServiceCollection AddStorage(this IServiceCollection services, StorageSettings storageSettings)
        {
            services.AddSingleton(storageSettings);
            services.AddScoped<BlobStorageFactory>();
            services.AddScoped<IAttachmentProvider, AttachmentProvider>();
            services.AddScoped<IAttachmentDirectoryProvider, AttachmentDirectoryProvider>();
            return services;
        }
    }
}
