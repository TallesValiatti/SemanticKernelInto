using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Configuration;

public class ProductConfiguration :  IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.UnitPrice)
            .IsRequired();
        
        builder.Property(e => e.Name)
            .IsRequired();
    }
}