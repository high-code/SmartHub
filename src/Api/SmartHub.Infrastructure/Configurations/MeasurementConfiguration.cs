using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHub.Domain.Entities;

namespace SmartHub.Domain.Configurations
{
  public class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
  {
    public void Configure(EntityTypeBuilder<Measurement> builder)
    {
      builder.HasKey(t => t.Id);
      builder.Property(t => t.Id).HasColumnName("id");
      builder.Property(t => t.Value).HasColumnName("value");
      builder.Property(t => t.DtSend).HasColumnName("dtsend");
      builder.Property(t => t.Type).HasColumnName("type");
      builder.Property(t => t.DeviceId).HasColumnName("deviceid");
      builder.ToTable("measurements").HasKey(t => t.Id);
    }
  }
}
