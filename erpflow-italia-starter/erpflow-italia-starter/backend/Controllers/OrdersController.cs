using ERPFlowItalia.Api.Data;
using ERPFlowItalia.Api.DTOs;
using ERPFlowItalia.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPFlowItalia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();

        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
    {
        if (!dto.Items.Any()) return BadRequest("Order must contain at least one item.");

        var order = new Order
        {
            CustomerId = dto.CustomerId,
            OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}",
            Status = OrderStatus.Draft
        };

        decimal net = 0;
        decimal vat = 0;

        foreach (var itemDto in dto.Items)
        {
            var product = await _context.Products.FindAsync(itemDto.ProductId);
            if (product == null) return BadRequest($"Product {itemDto.ProductId} not found.");

            var lineNet = product.UnitPrice * itemDto.Quantity;
            var lineVat = lineNet * product.VatRate / 100;
            var lineTotal = lineNet + lineVat;

            order.Items.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = itemDto.Quantity,
                UnitPrice = product.UnitPrice,
                VatRate = product.VatRate,
                LineTotal = lineTotal
            });

            net += lineNet;
            vat += lineVat;
        }

        order.NetAmount = net;
        order.VatAmount = vat;
        order.TotalAmount = net + vat;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return Ok(order);
    }

    [HttpPut("{id}/confirm")]
    public async Task<IActionResult> ConfirmOrder(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return NotFound();
        if (order.Status == OrderStatus.Confirmed)
        {
            return BadRequest("Order is already confirmed.");
        }

        foreach (var item in order.Items)
        {
            var stock = await _context.ProductStocks
                .FirstOrDefaultAsync(s => s.ProductId == item.ProductId);

            if (stock == null || stock.Quantity < item.Quantity)
                return BadRequest($"Not enough stock for product {item.ProductId}.");
        }

        foreach (var item in order.Items)
        {
            var stock = await _context.ProductStocks
                .FirstAsync(s => s.ProductId == item.ProductId);

            stock.Quantity -= item.Quantity;

            _context.StockMovements.Add(new StockMovement
            {
                ProductId = item.ProductId,
                WarehouseId = stock.WarehouseId,
                MovementType = StockMovementType.OUT,
                Quantity = item.Quantity,
                ReferenceNumber = order.OrderNumber,
                Reason = "Order confirmed"
            });
        }

        order.Status = OrderStatus.Confirmed;
        await _context.SaveChangesAsync();

        return Ok(order);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return NotFound();

        var hasInvoices = await _context.Invoices.AnyAsync(i => i.OrderId == id);
        if (hasInvoices)
        {
            return BadRequest("Delete related invoices before deleting the order.");
        }

        var stockMovements = await _context.StockMovements
            .Where(m => m.ReferenceNumber == order.OrderNumber)
            .ToListAsync();

        foreach (var stockMovement in stockMovements)
        {
            var productStock = await _context.ProductStocks
                .FirstOrDefaultAsync(s => s.ProductId == stockMovement.ProductId && s.WarehouseId == stockMovement.WarehouseId);

            if (productStock != null)
            {
                productStock.Quantity += stockMovement.Quantity;
            }
            else
            {
                _context.ProductStocks.Add(new ProductStock
                {
                    ProductId = stockMovement.ProductId,
                    WarehouseId = stockMovement.WarehouseId,
                    Quantity = stockMovement.Quantity
                });
            }
        }

        _context.StockMovements.RemoveRange(stockMovements);
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
