namespace ERPFlowItalia.Api.DTOs;

public class CreateProductDto
{
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public decimal VatRate { get; set; } = 22;
    public int MinimumStockLevel { get; set; } = 5;
    public int? SupplierId { get; set; }
}
