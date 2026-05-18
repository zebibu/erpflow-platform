using ERPFlowItalia.Desktop.Models;
using ERPFlowItalia.Desktop.Services;
using System.Windows.Controls;

namespace ERPFlowItalia.Desktop.Views;

public partial class StockView : UserControl, IRefreshable
{
    private readonly ApiClient _api = new();

    public StockView()
    {
        InitializeComponent();
        _ = RefreshAsync();
    }

    public async Task RefreshAsync()
    {
        StockGrid.ItemsSource = await _api.GetListAsync<StockItem>("stock");
    }
}
