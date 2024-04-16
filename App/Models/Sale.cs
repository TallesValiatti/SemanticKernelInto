namespace App.Models;

public class Sale
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public IList<SaleItem> SaleItems { get; set; } = default!;
}