﻿using CME.MultiTenancy.Abstractions.Providers;
using CME.MultiTenancy.Abstractions.Repositories;
using CME.MultiTenancy.Configurations;
using CME.MultiTenancy.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IngoX.MultiTenancy
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMultiTenancySupport<T>(this IServiceCollection services,
            IConfigurationRoot configuration) where T : class, ITenantProvider
        {
            var tenantSettings = configuration.GetSection(nameof(TenantSettings)).Get<TenantSettings>() ?? new TenantSettings();
            return services.AddMultiTenancySupport<T>(tenantSettings);
        }

        public static IServiceCollection AddMultiTenancySupport<T>(this IServiceCollection services,
            TenantSettings tenantSettings) where T : class, ITenantProvider
        {
            services.AddSingleton(tenantSettings);
            services.AddHttpContextAccessor();
            services.AddScoped<ITenantProvider, T>();
            return services;
        }

        /// <summary>
        /// Reads tenant definitions locally from the AppSettings.json and cache the in-memory.
        /// </summary>
        public static IServiceCollection AddLocalCache(this IServiceCollection services)
        {
            services.AddSingleton<ITenantRepository, LocalTenantRepository>();
            return services;
        }
    }
}
