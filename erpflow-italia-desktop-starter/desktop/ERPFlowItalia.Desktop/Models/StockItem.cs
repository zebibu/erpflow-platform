namespace ERPFlowItalia.Desktop.Models;

public class StockItem
{
    public int Id { get; set; }
    public string Product { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public string Warehouse { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int MinimumStockLevel { get; set; }
    public bool IsLowStock { get; set; }
}
