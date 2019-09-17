using System;
using Microsoft.EntityFrameworkCore;

namespace SmartHub.Identity.Helpers
{
  public static class ModelBuilderExtensions
  {

    public static void ToSnakeCaseNamingConventions(this ModelBuilder builder)
    {
      foreach (var entity in builder.Model.GetEntityTypes())
      {
        entity.Relational().TableName = entity.Relational().TableName.ToSnakeCaseInvariant();

        foreach (var prop in entity.GetProperties())
          prop.Relational().ColumnName = prop.Name.ToSnakeCaseInvariant();

        foreach (var key in entity.GetKeys())
          key.Relational().Name = key.Relational().Name.ToSnakeCaseInvariant();

        foreach (var foreignKey in entity.GetForeignKeys())
          foreignKey.Relational().Name = foreignKey.Relational().Name.ToSnakeCaseInvariant();

        foreach (var index in entity.GetIndexes())
          index.Relational().Name = index.Relational().Name.ToSnakeCaseInvariant();

      }
    }
  }
}
