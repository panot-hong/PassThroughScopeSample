namespace PassThroughScopeSample
{
    public class LogService<T> : ILogService<T>
    {
        private readonly ILogger<T> _logger;
        private readonly TenantIdentifier _tenantIdentifier;

        public LogService(ILogger<T> logger, TenantIdentifier tenantIdentifier)
        {
            _logger = logger;
            _tenantIdentifier = tenantIdentifier;
        }

        public async Task TenantAuditAsync(string action)
        {
            var logMessage = $"Tenant [{_tenantIdentifier.TenantId}] is being [{action}]";
            Console.WriteLine(logMessage);
            _logger.LogInformation(logMessage);
            await Task.Delay(1000); // simmulate long-process action like save audit log to the database
        }
    }
}
