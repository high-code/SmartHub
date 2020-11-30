using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHub.Edge.Domain.MeasurementAggregate;

namespace SmartHub.Edge.Infrastructure.Configuration
{
  public class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
  {
    public void Configure(EntityTypeBuilder<Measurement> builder)
    {
      builder.HasKey(m => m.Id);
      builder.Property(m => m.Id).HasColumnName("id");
      builder.Property(m => m.DeviceId).HasColumnName("device_id");
      builder.Property(m => m.DtSent).HasColumnName("dt_sent");
      builder.Property(m => m.DtReceived).HasColumnName("dt_received");
      builder.Property(m => m.Type).HasColumnName("type");
      builder.Property(m => m.Value).HasColumnName("value");
      builder.ToTable("measurements");
    }
  }
}
