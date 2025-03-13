using CME.Common;
using CME.MultiTenancy.Abstractions.Repositories;
using CME.MultiTenancy.Services;
using MassTransit;
using MassTransit.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace CME.MessageBroker.Services
{
    public class MessageBrokerTenantProvider : HttpTenantProvider
    {
        private readonly ScopedConsumeContextProvider _scopedConsumeContextProvider;

        public MessageBrokerTenantProvider(IHttpContextAccessor httpContextAccessor,
            ITenantRepository tenantRepository,
            ScopedConsumeContextProvider scopedConsumeContextProvider) : base(httpContextAccessor, tenantRepository)
        {
            _scopedConsumeContextProvider = scopedConsumeContextProvider;
        }

        public override string GetTenantId()
        {
            var tenantId = base.GetTenantId(); // get tenant from http request if any
            if (string.IsNullOrWhiteSpace(tenantId))
            {
                if (_scopedConsumeContextProvider != null)
                {
                    ConsumeContext scopedConsumeContextProvider = _scopedConsumeContextProvider.GetContext();
                    if (scopedConsumeContextProvider != null &&
                        scopedConsumeContextProvider.Headers.TryGetHeader(Constants.Headers.TenantId, out var header))
                    {
                        return header.ToString();
                    }
                }
            }
            return tenantId;
        }
    }
}
