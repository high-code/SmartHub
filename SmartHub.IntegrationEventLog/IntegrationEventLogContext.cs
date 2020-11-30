using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHub.DataAccess;

namespace SmartHub.IntegrationEventLog
{
  public class IntegrationEventLogContext : DbContext
  {

    public IntegrationEventLogContext(DbContextOptions options) : base(options) { }

    public DbSet<IntegrationEventLogEntry> IntegrationEventLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<IntegrationEventLogEntry>(ConfigureIntegrationEventLogEntry);
      modelBuilder.ToSnakeCaseNamingConventions();
    }

    private void ConfigureIntegrationEventLogEntry(EntityTypeBuilder<IntegrationEventLogEntry> builder)
    {
      builder.ToTable("integration_event_log");

      builder.HasKey(e => e.EventId);

      builder.Property(e => e.EventId).IsRequired();

      builder.Property(e => e.EventState).IsRequired();

      builder.Property(e => e.Content).IsRequired();

      builder.Property(e => e.CreatedTime).IsRequired();

      builder.Property(e => e.EventTypeName).IsRequired();

    }
  }

  public class EdgeDbContextDesignFactory : IDesignTimeDbContextFactory<IntegrationEventLogContext>
  {
    public IntegrationEventLogContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventLogContext>()
        .UseNpgsql("User ID = postgres;Password=admin;Server=localhost;Port=5432;Database=smarthub_edge;Integrated Security=true; Pooling=true;", options =>
        {
          
          options.MigrationsAssembly("SmartHub.Edge");
        });
        
      return new IntegrationEventLogContext(optionsBuilder.Options);
    }
  }
}
