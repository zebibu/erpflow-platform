using ERPFlowItalia.Api.Data;
using ERPFlowItalia.Api.DTOs;
using ERPFlowItalia.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPFlowItalia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly AppDbContext _context;

    public StockController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetStock()
    {
        var stock = await _context.ProductStocks
            .Include(s => s.Product)
            .Include(s => s.Warehouse)
            .OrderBy(s => s.Product!.Name)
            .Select(s => new
            {
                s.Id,
                Product = s.Product!.Name,
                s.Product.SKU,
                Warehouse = s.Warehouse!.Name,
                s.Quantity,
                s.Product.MinimumStockLevel,
                IsLowStock = s.Quantity <= s.Product.MinimumStockLevel
            })
            .ToListAsync();

        return Ok(stock);
    }

    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStock()
    {
        var lowStock = await _context.ProductStocks
            .Include(s => s.Product)
            .Include(s => s.Warehouse)
            .Where(s => s.Quantity <= s.Product!.MinimumStockLevel)
            .ToListAsync();

        return Ok(lowStock);
    }

    [HttpPost("movement")]
    public async Task<IActionResult> CreateMovement(CreateStockMovementDto dto)
    {
        if (dto.Quantity <= 0) return BadRequest("Quantity must be greater than zero.");

        var stock = await _context.ProductStocks
            .FirstOrDefaultAsync(s => s.ProductId == dto.ProductId && s.WarehouseId == dto.WarehouseId);

        if (stock == null)
        {
            stock = new ProductStock
            {
                ProductId = dto.ProductId,
                WarehouseId = dto.WarehouseId,
                Quantity = 0
            };
            _context.ProductStocks.Add(stock);
        }

        if (dto.MovementType == StockMovementType.IN || dto.MovementType == StockMovementType.RETURN)
        {
            stock.Quantity += dto.Quantity;
        }
        else if (dto.MovementType == StockMovementType.OUT)
        {
            if (stock.Quantity < dto.Quantity)
                return BadRequest("Not enough stock available.");

            stock.Quantity -= dto.Quantity;
        }
        else if (dto.MovementType == StockMovementType.ADJUSTMENT)
        {
            stock.Quantity = dto.Quantity;
        }

        var movement = new StockMovement
        {
            ProductId = dto.ProductId,
            WarehouseId = dto.WarehouseId,
            MovementType = dto.MovementType,
            Quantity = dto.Quantity,
            ReferenceNumber = dto.ReferenceNumber,
            Reason = dto.Reason
        };

        _context.StockMovements.Add(movement);
        await _context.SaveChangesAsync();

        return Ok(movement);
    }

    [HttpGet("movements")]
    public async Task<IActionResult> GetMovements()
    {
        var movements = await _context.StockMovements
            .Include(m => m.Product)
            .Include(m => m.Warehouse)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();

        return Ok(movements);
    }
}
