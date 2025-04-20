using Localize.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Localize.Company.Infrastructure.Contexts
{
    public class LocalizeCompanyContext : DbContext
    {
        public LocalizeCompanyContext(DbContextOptions<LocalizeCompanyContext> options ) :base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var properties = modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetProperties());
            foreach(var property in properties)
            {
                if (property.ClrType == typeof(string))
                    property.SetColumnType("varchar(255)");
                if (property.ClrType == typeof(DateTime))
                    property.SetColumnType("datetime2(3)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocalizeCompanyContext).Assembly);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();
                entity.Property(x => x.Name).IsRequired(true);
                entity.Property(x => x.Email).IsRequired();
                entity.Property(x => x.Password).IsRequired();
            });

            
            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();
                entity.Property(x => x.Cnpj).IsRequired(true);
                entity.Property(x => x.UserId).IsRequired(true);

                //Config One to Many
                entity.HasOne<User>()
                    .WithMany(u => u.Organizations)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Organization>()
                .OwnsOne(o => o.Endereco);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreateDate") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("CreateDate").CurrentValue = DateTime.UtcNow;

                if (entry.State == EntityState.Modified)
                    entry.Property("CreateDate").IsModified = false;
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UpdateDate") != null))
            {
                if (entry.State == EntityState.Modified)
                    entry.Property("UpdateDate").CurrentValue = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
