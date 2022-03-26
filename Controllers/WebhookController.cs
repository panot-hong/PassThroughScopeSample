using Microsoft.AspNetCore.Mvc;

namespace PassThroughScopeSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly TenantIdentifier _tenantIdentifier;
        private readonly IServiceProvider _serviceProvider;

        public WebhookController(TenantIdentifier tenantIdentifier, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _tenantIdentifier = tenantIdentifier;
        }

        [HttpPost]
        public async Task Post([FromBody]WebhookPayload webhookPayload)
        {
            _tenantIdentifier.TenantId = webhookPayload.TenantId;
            // Using Task.Run to fire and forget logging operation which is not too important
            _ = Task.Run(async () => // This can be one-liner just expose to explain
            {
                // Create a new scope to avoid scope is disposed sooner than this to be completed
                var scope = _serviceProvider.CreateServiceScope(); // Create a new scope using CreateServiceScope() instead of CreateScope() to carry data
                var logService = scope.ServiceProvider.GetRequiredService<ILogService<WebhookController>>();
                await logService.TenantAuditAsync(webhookPayload.Action);
            });
            await Task.Delay(500); // simmulate actual long-operation
        }
    }
}