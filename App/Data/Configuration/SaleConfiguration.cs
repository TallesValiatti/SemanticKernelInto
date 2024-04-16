using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Configuration;

public class SaleConfiguration :  IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        
        builder.HasMany(e => e.SaleItems)
            .WithOne(x => x.Sale)
            .HasForeignKey(x =>  x.SaleId);
    }
}