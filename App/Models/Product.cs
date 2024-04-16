namespace App.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal UnitPrice { get; set; } = default!;
}