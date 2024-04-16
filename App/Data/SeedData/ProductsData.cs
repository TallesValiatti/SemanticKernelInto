using App.Models;

namespace App.Data.SeedData;

public static class ProductsData
{
    private static Product? _soap;
    private static Product? _chocolateBar;
    private static Product? _ketchup;

    public static Product Soap
    {
        get
        {
            if (_soap == null)
            {
                _soap = new Product
                {
                    Id = new Guid("5D3F102C-3EEC-4B84-B406-6FDD9E02824D"),
                    Name = "Soap",
                    UnitPrice = (decimal)2.50
                };
            }

            return _soap;
        }
    }
    
    public static Product ChocolateBar
    {
        get
        {
            return _chocolateBar ??= new Product
            {
                Id = new Guid("0E489625-E9AD-46C7-A92B-7F9BFD434D57"),
                Name = "Chocolate bar",
                UnitPrice = (decimal)1.25
            };
        }
    }
    
    public static Product Ketchup
    {
        get
        {
            return _ketchup ??= new Product
            {
                Id = new Guid("6790C600-7C75-4EF8-97FB-04F3E6F30F8A"),
                Name = "Ketchup",
                UnitPrice = (decimal)3.00
            };
        }
    }
}