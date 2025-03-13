using SkeletonKit.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace SkeletonKit.Common.Logging
{
    internal class CustomEnricherSerilogMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomEnricherSerilogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            string userId = context.User?.Claims?.GetValue<string>(Constants.Claims.SubjectId);
            string userName = context.User?.Claims?.GetValue<string>(Constants.Claims.UserName);

            context.Request.Headers.TryGetValue(Constants.Headers.TenantId, out var tenantId);
            context.Request.Headers.TryGetValue(Constants.Headers.TimeZone, out var timeZone);
            context.Request.Headers.TryGetValue(Constants.Headers.AppName, out var appName);
            context.Request.Headers.TryGetValue(Constants.Headers.OsName, out var osName);
            context.Request.Headers.TryGetValue(Constants.Headers.AppVersion, out var appVersion);

            if (string.IsNullOrEmpty(userId)) userId = Constants.System;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { nameof(userId), userId },
            };

            if (!string.IsNullOrEmpty(tenantId)) data.Add(nameof(tenantId), tenantId);
            if (!string.IsNullOrEmpty(userName)) data.Add(nameof(userName), userName);
            if (!string.IsNullOrEmpty(timeZone)) data.Add(nameof(timeZone), timeZone);
            if (!string.IsNullOrEmpty(appName)) data.Add(nameof(appName), appName);
            if (!string.IsNullOrEmpty(osName)) data.Add(nameof(osName), osName);
            if (!string.IsNullOrEmpty(appVersion)) data.Add(nameof(appVersion), appVersion);

            using (LogContext.Push(new AddPropertiesEnricher(data)))
            {
                return _next(context);
            }
        }
    }
}
