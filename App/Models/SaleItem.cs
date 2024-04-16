namespace App.Models;

public class SaleItem
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid SaleId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
    public Sale Sale { get; set; } = default!;
}