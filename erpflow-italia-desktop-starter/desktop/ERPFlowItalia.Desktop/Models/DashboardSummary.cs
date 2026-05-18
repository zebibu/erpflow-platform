namespace ERPFlowItalia.Desktop.Models;

public class DashboardSummary
{
    public int TotalProducts { get; set; }
    public int TotalCustomers { get; set; }
    public int TotalOrders { get; set; }
    public int TotalInvoices { get; set; }
    public int LowStock { get; set; }
    public decimal SalesTotal { get; set; }
}
