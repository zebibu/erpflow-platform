namespace ERPFlowItalia.Api.Models;

public enum PaymentStatus
{
    Unpaid,
    Paid,
    Overdue,
    Cancelled
}

public class Invoice
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
    public decimal NetAmount { get; set; }
    public decimal VatAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
    public string PdfUrl { get; set; } = string.Empty;
}
