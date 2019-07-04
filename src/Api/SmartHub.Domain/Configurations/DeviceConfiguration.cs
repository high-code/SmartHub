using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SmartHub.Domain.Entities;

namespace SmartHub.Domain.Configurations
{
  public class DeviceConfiguration : IEntityTypeConfiguration<Device>
  {
    public void Configure(EntityTypeBuilder<Device> builder)
    {
      builder.HasKey(d => d.Id);
      builder.HasAlternateKey(d => d.DeviceId);
      builder.Property(d => d.Id).HasColumnName("id");
      builder.Property(d => d.Name).HasColumnName("name");
      builder.ToTable("devices").HasKey(d => d.Id);
    }
  }
}
