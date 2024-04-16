using App.Models;

namespace App.Data.SeedData;

public static class SaleData
{
    private static Sale? _saleOne;
    private static Sale? _saleTwo;
    private static Sale? _saleThree;
    
    public static Sale SaleOne
    {
        get
        {
            return _saleOne ??= new Sale
            {
                Id = new Guid("6790C600-7C75-4EF8-97FB-04F3E6F30F8A"),
                CreatedAt = new DateTime(2024,2, 2, 13, 0, 0),
                SaleItems = new List<SaleItem>
                {
                    new SaleItem
                    {
                        ProductId = ProductsData.Ketchup.Id,
                        UnitPrice = ProductsData.Ketchup.UnitPrice,
                        Units = 1
                    },
                    new SaleItem
                    {
                        ProductId = ProductsData.ChocolateBar.Id,
                        UnitPrice = ProductsData.ChocolateBar.UnitPrice,
                        Units = 5
                    },
                    new SaleItem
                    {
                        ProductId = ProductsData.Soap.Id,
                        UnitPrice = ProductsData.Soap.UnitPrice,
                        Units = 10
                    }
                }
            };
        }
    }
    
    public static Sale SaleTwo
    {
        get
        {
            return _saleTwo ??= new Sale
            {
                Id = new Guid("36D2D7B8-0EAD-47A5-BAA5-1229ECBF7DD4"),
                CreatedAt = new DateTime(2024,1, 6, 12, 0, 0),
                SaleItems = new List<SaleItem>
                {
                    new SaleItem
                    {
                        ProductId = ProductsData.Ketchup.Id,
                        UnitPrice = ProductsData.Ketchup.UnitPrice,
                        Units = 10
                    }
                }
            };
        }
    }
    
    public static Sale SaleThree
    {
        get
        {
            return _saleThree ??= new Sale
            {
                Id = new Guid("B2A83A58-8C34-4BE1-95D1-41B3498CDE8C"),
                CreatedAt = new DateTime(2023,12, 6, 12, 0, 0),
                SaleItems = new List<SaleItem>
                {
                    new SaleItem
                    {
                        ProductId = ProductsData.ChocolateBar.Id,
                        UnitPrice = ProductsData.ChocolateBar.UnitPrice,
                        Units = 4
                    }
                }
            };
        }
    }
}