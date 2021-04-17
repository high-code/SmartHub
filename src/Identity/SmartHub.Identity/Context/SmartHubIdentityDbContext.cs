using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHub.Identity.Identity;
using SmartHub.DataAccess;
using Microsoft.EntityFrameworkCore.Design;

namespace SmartHub.Identity.Context
{
  public class SmartHubIdentityDbContext : IdentityDbContext<ApplicationUser>
  {

    public SmartHubIdentityDbContext(DbContextOptions<SmartHubIdentityDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.HasDefaultSchema("idn");

      builder.Entity<ApplicationUser>(b => { b.ToTable("users"); });

      builder.Entity<IdentityUserClaim<string>>(b => { b.ToTable("user_claims"); });

      builder.Entity<IdentityUserLogin<string>>(b => { b.ToTable("user_logins"); });

      builder.Entity<IdentityRole>(b => { b.ToTable("roles"); });

      builder.Entity<IdentityRole>().HasData(new IdentityRole {Name = "Admin", NormalizedName = "Admin".ToUpper()});

      builder.Entity<IdentityUserToken<string>>(b => { b.ToTable("user_tokens"); });

      builder.Entity<IdentityRoleClaim<string>>(b => { b.ToTable("role_claims"); });

      builder.Entity<IdentityUserRole<string>>(b => { b.ToTable("user_roles"); });

      builder.ToSnakeCaseNamingConventions();

    }
  }

  public class SmartHubIdentityDbContextDesignFactory : IDesignTimeDbContextFactory<SmartHubIdentityDbContext>
  {
    public SmartHubIdentityDbContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<SmartHubIdentityDbContext>()
        .UseNpgsql("User ID = postgres;Password=admin;Server=localhost;Port=5432;Database=smarthub_identity;Integrated Security=true; Pooling=true;", options =>
        {

          options.MigrationsAssembly("SmartHub.Identity");
        });

      return new SmartHubIdentityDbContext(optionsBuilder.Options);
    }
  }
}
