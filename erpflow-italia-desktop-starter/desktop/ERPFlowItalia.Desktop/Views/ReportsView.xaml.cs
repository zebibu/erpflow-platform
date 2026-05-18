using ERPFlowItalia.Desktop.Models;
using ERPFlowItalia.Desktop.Services;
using System.Windows.Controls;

namespace ERPFlowItalia.Desktop.Views;

public partial class ReportsView : UserControl, IRefreshable
{
    private readonly ApiClient _api = new();

    public ReportsView()
    {
        InitializeComponent();
        _ = RefreshAsync();
    }

    public async Task RefreshAsync()
    {
        ReportsGrid.ItemsSource = await _api.GetListAsync<StockReportItem>("reports/stock");
    }
}