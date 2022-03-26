namespace PassThroughScopeSample
{
    public interface ILogService<T>
    {
        Task TenantAuditAsync(string action);
    }
}
