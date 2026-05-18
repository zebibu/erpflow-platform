using ERPFlowItalia.Desktop.Models;
using ERPFlowItalia.Desktop.Services;
using System.Windows.Controls;

namespace ERPFlowItalia.Desktop.Views;

public partial class DashboardView : UserControl, IRefreshable
{
    private readonly ApiClient _api = new();

    public DashboardView()
    {
        InitializeComponent();
        _ = RefreshAsync();
    }

    public async Task RefreshAsync()
    {
        var data = await _api.GetAsync<DashboardSummary>("reports/dashboard");

        if (data == null) return;

        ProductsText.Text = data.TotalProducts.ToString();
        CustomersText.Text = data.TotalCustomers.ToString();
        OrdersText.Text = data.TotalOrders.ToString();
        InvoicesText.Text = data.TotalInvoices.ToString();
        LowStockText.Text = data.LowStock.ToString();
        SalesText.Text = $"€{data.SalesTotal:N2}";
    }
}
