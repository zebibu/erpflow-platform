using ERPFlowItalia.Desktop.Models;
using ERPFlowItalia.Desktop.Services;
using System.Windows;
using System.Windows.Controls;

namespace ERPFlowItalia.Desktop.Views;

public partial class ProductsView : UserControl, IRefreshable
{
    private readonly ApiClient _api = new();

    public ProductsView()
    {
        InitializeComponent();
        _ = RefreshAsync();
    }

    private async void Load_Click(object sender, RoutedEventArgs e)
    {
        await RefreshAsync();
    }

    public async Task RefreshAsync()
    {
        ProductsGrid.ItemsSource = await _api.GetListAsync<Product>("products");
    }
}
