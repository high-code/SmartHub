using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHub.Identity.Helpers;
using SmartHub.Identity.Identity;

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
}
