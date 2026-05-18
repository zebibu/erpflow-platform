using ERPFlowItalia.Api.Models;

namespace ERPFlowItalia.Api.DTOs;

public class CreateStockMovementDto
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public StockMovementType MovementType { get; set; }
    public int Quantity { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
}
