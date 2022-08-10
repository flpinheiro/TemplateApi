using Microsoft.EntityFrameworkCore;
using TemplateApi.Domain.Interfaces;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Infra.Context.Mappings;

namespace TemplateApi.Infra.Context;

public class TemplateApiContext : DbContext
{
    public DbSet<Person> People { get; set; }

    public TemplateApiContext(DbContextOptions<TemplateApiContext> options) : base(options)
    {
        //if (options == null) throw new ArgumentNullException(nameof(options));
        People = Set<Person>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new PersonMap());
    }

    #region Override SaveChanges
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        TackLastModifiedDate();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        TackLastModifiedDate();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void TackLastModifiedDate()
    {
        ChangeTracker.DetectChanges();
        var now = DateTime.UtcNow;

        foreach (var item in ChangeTracker.Entries<IAuditable>().Where(e => e.State == EntityState.Added))
        {
            item.Property(u => u.LastUpdate).CurrentValue = now;
        }

        foreach (var item in ChangeTracker.Entries<IAuditable>().Where(e => e.State == EntityState.Modified))
        {
            item.Property(u => u.LastUpdate).CurrentValue = now;
            item.Property(u => u.IsUpdated).CurrentValue = true;
        }
    }
    #endregion
}
