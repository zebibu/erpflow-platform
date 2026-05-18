using ERPFlowItalia.Api.Data;
using ERPFlowItalia.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPFlowItalia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReportsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        var totalProducts = await _context.Products.CountAsync();
        var totalCustomers = await _context.Customers.CountAsync();
        var totalOrders = await _context.Orders.CountAsync();
        var totalInvoices = await _context.Invoices.CountAsync();
        var lowStock = await _context.ProductStocks
            .Include(s => s.Product)
            .CountAsync(s => s.Quantity <= s.Product!.MinimumStockLevel);

        var salesTotal = await _context.Orders
            .Where(o => o.Status != OrderStatus.Cancelled)
            .Select(o => (decimal?)o.TotalAmount)
            .SumAsync() ?? 0m;

        return Ok(new
        {
            totalProducts,
            totalCustomers,
            totalOrders,
            totalInvoices,
            lowStock,
            salesTotal
        });
    }

    [HttpGet("stock")]
    public async Task<IActionResult> GetStockReport()
    {
        var report = await _context.ProductStocks
            .Include(s => s.Product)
            .Include(s => s.Warehouse)
            .Select(s => new
            {
                Product = s.Product!.Name,
                s.Product.SKU,
                Warehouse = s.Warehouse!.Name,
                s.Quantity,
                StockValue = s.Quantity * s.Product.UnitPrice
            })
            .ToListAsync();

        return Ok(report);
    }
}
