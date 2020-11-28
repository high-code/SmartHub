using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
    }

    private void ConfigureIntegrationEventLogEntry(EntityTypeBuilder<IntegrationEventLogEntry> builder)
    {
      builder.ToTable("IntegrationEventLog");

      builder.HasKey(e => e.EventId);

      builder.Property(e => e.EventId).IsRequired();

      builder.Property(e => e.EventState).IsRequired();

      builder.Property(e => e.Content).IsRequired();

      builder.Property(e => e.CreatedTime).IsRequired();

      builder.Property(e => e.EventTypeName).IsRequired();

    }
  }
}
