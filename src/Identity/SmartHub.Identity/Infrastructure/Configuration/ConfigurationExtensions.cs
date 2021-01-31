using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHub.Identity.Infrastructure.Entities;

namespace SmartHub.Identity.Infrastructure.Configuration
{
  public static class IdentityServerDbContectConfiguration
  {

    public static void ConfigureClientEntity(EntityTypeBuilder<IdentityServerClient> clientEntityBuilder)
    {
      clientEntityBuilder.ToTable("clients");
      clientEntityBuilder.HasKey(x => x.Id);

      //clientEntityBuilder.Property(x => x.ClientId).HasMaxLength(200).IsRequired();
      //clientEntityBuilder.Property(x => x.ClientName).HasMaxLength(200);
      //clientEntityBuilder.Property(x => x.UserId);

      //clientEntityBuilder.HasIndex(x => x.ClientId).IsUnique();
      //clientEntityBuilder.HasIndex(x => x.UserId);

      //clientEntityBuilder.HasMany(x => x.AllowedGrantTypes).WithOne(x => x.Client).IsRequired();
      //clientEntityBuilder.HasMany(x => x.RedirectUris).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired();
      //clientEntityBuilder.HasMany(x => x.PostLogoutRedirectUris).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired();
      //clientEntityBuilder.HasMany(x => x.AllowedScopes).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired();
      //clientEntityBuilder.HasMany(x => x.ClientSecrets).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired();
    }

    public static void ConfigureClientGrantTypeEntity(EntityTypeBuilder<ClientGrantType> clientGrantTypeEntityBuilder)
    {
      clientGrantTypeEntityBuilder.ToTable("client_grant_types");
      clientGrantTypeEntityBuilder.HasKey(x => x.Id);
      clientGrantTypeEntityBuilder.Property(x => x.GrantType).HasMaxLength(250).IsRequired();
    }

    public static void ConfigureClientPostLogoutRedirectUriEntity(EntityTypeBuilder<ClientPostLogoutRedirectUri> clientPostLogoutRedirectUriEntityBuilder)
    {
      clientPostLogoutRedirectUriEntityBuilder.ToTable("client_postlogout_redirect_uris");
      clientPostLogoutRedirectUriEntityBuilder.HasKey(x => x.Id);
      clientPostLogoutRedirectUriEntityBuilder.Property(x => x.PostLogoutRedirectUri).HasMaxLength(2000).IsRequired();
    }

    public static void ConfigureClientRedirectUriEntity(EntityTypeBuilder<ClientRedirectUri> clientRedirectUriEntityTypeBuilder)
    {
      clientRedirectUriEntityTypeBuilder.ToTable("client_redirect_uris");
      clientRedirectUriEntityTypeBuilder.HasKey(x => x.Id);
      clientRedirectUriEntityTypeBuilder.Property(x => x.RedirectUri).HasMaxLength(2000).IsRequired();
    }

    public static void ConfigureClientScopeEntity(EntityTypeBuilder<ClientScope> clientScopeEntityTypeBuilder)
    {
      clientScopeEntityTypeBuilder.ToTable("client_scopes");
      clientScopeEntityTypeBuilder.HasKey(x => x.Id);
      clientScopeEntityTypeBuilder.Property(x => x.Scope).HasMaxLength(250).IsRequired();
    }

    public static void ConfigureClientSecretEntity(EntityTypeBuilder<ClientSecret> clientSecretEntityTypeBuilder)
    {
      clientSecretEntityTypeBuilder.ToTable("client_secrets");
      clientSecretEntityTypeBuilder.HasKey(x => x.Id);
      clientSecretEntityTypeBuilder.Property(x => x.Value).HasMaxLength(4000).IsRequired();
    }
  }
}
