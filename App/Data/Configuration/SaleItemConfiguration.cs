using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Configuration;

public class SaleItemConfiguration :  IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.ProductId)
            .IsRequired();
        
        builder.Property(e => e.UnitPrice)
            .IsRequired();
        
        builder.Property(e => e.Units)
            .IsRequired();
    }
}