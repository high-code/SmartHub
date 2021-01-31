using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SmartHub.DataAccess;
using SmartHub.Identity.Infrastructure.Entities;
using static SmartHub.Identity.Infrastructure.Configuration.IdentityServerDbContectConfiguration;
namespace SmartHub.Identity.Infrastructure
{
  public class IdentityServerDbContext : DbContext
  {
    public DbSet<IdentityServerClient> Clients { get; set; }

    public DbSet<ClientGrantType> ClientGrantTypes { get; set; }

    public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }

    public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }

    public DbSet<ClientScope> ClientScopes { get; set; }

    public DbSet<ClientSecret> ClientSecrets { get; set; }

    public IdentityServerDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<IdentityServerClient>(ConfigureClientEntity);
      modelBuilder.Entity<ClientGrantType>(ConfigureClientGrantTypeEntity);
      modelBuilder.Entity<ClientPostLogoutRedirectUri>(ConfigureClientPostLogoutRedirectUriEntity);
      modelBuilder.Entity<ClientRedirectUri>(ConfigureClientRedirectUriEntity);
      modelBuilder.Entity<ClientScope>(ConfigureClientScopeEntity);
      modelBuilder.Entity<ClientSecret>(ConfigureClientSecretEntity);
      modelBuilder.ToSnakeCaseNamingConventions();

      base.OnModelCreating(modelBuilder);
    }
  }

  public class IdentityServerDbContextDesignFactory : IDesignTimeDbContextFactory<IdentityServerDbContext>
  {
    public IdentityServerDbContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<IdentityServerDbContext>()
        .UseNpgsql("User ID = postgres;Password=admin;Server=localhost;Port=5432;Database=smarthub_identity;Integrated Security=true; Pooling=true;", options =>
        {

          options.MigrationsAssembly("SmartHub.Identity");
        });

      return new IdentityServerDbContext(optionsBuilder.Options);
    }
  }
}
