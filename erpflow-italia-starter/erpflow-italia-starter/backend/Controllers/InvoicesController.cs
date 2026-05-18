using ERPFlowItalia.Api.Data;
using ERPFlowItalia.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPFlowItalia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly AppDbContext _context;

    public InvoicesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetInvoices()
    {
        var invoices = await _context.Invoices
            .Include(i => i.Order)
            .ThenInclude(o => o!.Customer)
            .OrderByDescending(i => i.InvoiceDate)
            .ToListAsync();

        return Ok(invoices);
    }

    [HttpPost("from-order/{orderId}")]
    public async Task<IActionResult> CreateInvoiceFromOrder(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null) return NotFound("Order not found.");

        var existingInvoice = await _context.Invoices.FirstOrDefaultAsync(i => i.OrderId == orderId);
        if (existingInvoice != null) return BadRequest("An invoice already exists for this order.");

        var invoice = new Invoice
        {
            OrderId = order.Id,
            InvoiceNumber = $"FT-{DateTime.UtcNow:yyyyMMddHHmmss}",
            NetAmount = order.NetAmount,
            VatAmount = order.VatAmount,
            TotalAmount = order.TotalAmount,
            PaymentStatus = PaymentStatus.Unpaid
        };

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        return Ok(invoice);
    }

    [HttpPut("{id}/payment-status")]
    public async Task<IActionResult> UpdatePaymentStatus(int id, [FromBody] PaymentStatus paymentStatus)
    {
        var invoice = await _context.Invoices.FindAsync(id);
        if (invoice == null) return NotFound();

        invoice.PaymentStatus = paymentStatus;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(int id)
    {
        var invoice = await _context.Invoices.FindAsync(id);
        if (invoice == null) return NotFound();

        _context.Invoices.Remove(invoice);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
