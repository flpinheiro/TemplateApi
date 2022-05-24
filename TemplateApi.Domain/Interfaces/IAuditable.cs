namespace TemplateApi.Domain.Interfaces
{
    public interface IAuditable
    {
        DateTime LastUpdate { get; set; }
        bool IsUpdated { get; set; }
    }
}
