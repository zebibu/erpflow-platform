namespace ERPFlowItalia.Desktop.Models;

public class StockReportItem
{
    public string Product { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public string Warehouse { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal StockValue { get; set; }
}