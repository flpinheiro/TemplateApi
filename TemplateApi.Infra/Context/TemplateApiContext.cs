using Microsoft.EntityFrameworkCore;
using TemplateApi.Domain.Interfaces;
using TemplateApi.Domain.Models.Dal;
using TemplateApi.Infra.Context.Mappings;

namespace TemplateApi.Infra.Context
{
    public class TemplateApiContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public TemplateApiContext(DbContextOptions<TemplateApiContext> options) : base(options)
        {
            People = Set<Person>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMap());

            base.OnModelCreating(modelBuilder);
        }

        #region Override SaveChanges
        public override int SaveChanges()
        {
            TackLastModifiedDate();
            return base.SaveChanges();
        }
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
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            TackLastModifiedDate();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void TackLastModifiedDate()
        {
            ChangeTracker.DetectChanges();
            var now = DateTime.UtcNow;

            foreach (var item in ChangeTracker.Entries<IAuditable>().Where(e => e.State == EntityState.Added))
            {
                item.Property(u => u.CreateDate).CurrentValue = now;
            }

            foreach (var item in ChangeTracker.Entries<IAuditable>().Where(e => e.State == EntityState.Modified))
            {
                item.Property(u => u.UpdateDate).CurrentValue = now;
            }
        }
        #endregion
    }
}
