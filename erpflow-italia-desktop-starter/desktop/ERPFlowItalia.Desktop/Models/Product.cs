namespace ERPFlowItalia.Desktop.Models;

public class Product
{
    public int Id { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public decimal VatRate { get; set; }
    public int MinimumStockLevel { get; set; }
}
