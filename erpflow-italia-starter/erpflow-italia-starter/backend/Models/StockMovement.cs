namespace ERPFlowItalia.Api.Models;

public enum StockMovementType
{
    IN,
    OUT,
    TRANSFER,
    ADJUSTMENT,
    RETURN
}

public class StockMovement
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }
    public StockMovementType MovementType { get; set; }
    public int Quantity { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
