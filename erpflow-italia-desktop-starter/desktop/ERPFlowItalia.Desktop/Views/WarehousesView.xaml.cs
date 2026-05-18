using ERPFlowItalia.Desktop.Models;
using ERPFlowItalia.Desktop.Services;
using System.Windows.Controls;

namespace ERPFlowItalia.Desktop.Views;

public partial class WarehousesView : UserControl, IRefreshable
{
    private readonly ApiClient _api = new();

    public WarehousesView()
    {
        InitializeComponent();
        _ = RefreshAsync();
    }

    public async Task RefreshAsync()
    {
        WarehousesGrid.ItemsSource = await _api.GetListAsync<Warehouse>("warehouses");
    }
}
