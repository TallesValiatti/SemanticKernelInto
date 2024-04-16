using Microsoft.EntityFrameworkCore;

namespace App.Data.SeedData;

public class SeedManager(AppDbContext context)
{
    public async Task SeedAsync() 
    {
        // Products
        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddAsync(ProductsData.Ketchup);
            await context.Products.AddAsync(ProductsData.ChocolateBar);
            await context.Products.AddAsync(ProductsData.Soap);
        }
        
        // Sales
        if (!await context.Sales.AnyAsync())
        {
            await context.Sales.AddAsync(SaleData.SaleOne);
            await context.Sales.AddAsync(SaleData.SaleTwo);
            await context.Sales.AddAsync(SaleData.SaleThree);
        }

        await context.SaveChangesAsync();
    }
}