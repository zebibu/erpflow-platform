namespace ERPFlowItalia.Api.Models;

public class Product
{
    public int Id { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public decimal VatRate { get; set; } = 22;
    public int MinimumStockLevel { get; set; } = 5;
    public int? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
}
