using ERPFlowItalia.Desktop.Models;
using ERPFlowItalia.Desktop.Services;
using System.Windows;
using System.Windows.Controls;

namespace ERPFlowItalia.Desktop.Views;

public partial class OrdersView : UserControl, IRefreshable
{
    private readonly ApiClient _api = new();

    public OrdersView()
    {
        InitializeComponent();
        _ = RefreshAsync();
    }

    public async Task RefreshAsync()
    {
        var customersTask = _api.GetListAsync<Customer>("customers");
        var productsTask = _api.GetListAsync<Product>("products");
        var ordersTask = _api.GetListAsync<Order>("orders");

        await Task.WhenAll(customersTask, productsTask, ordersTask);

        CustomerComboBox.ItemsSource = await customersTask;
        ProductComboBox.ItemsSource = await productsTask;
        OrdersGrid.ItemsSource = await ordersTask;
    }

    private async void CreateOrder_Click(object sender, RoutedEventArgs e)
    {
        if (CustomerComboBox.SelectedItem is not Customer customer || ProductComboBox.SelectedItem is not Product product)
        {
            MessageBox.Show("Select a customer and product first.");
            return;
        }

        if (!int.TryParse(QuantityTextBox.Text, out var quantity) || quantity <= 0)
        {
            MessageBox.Show("Quantity must be a positive number.");
            return;
        }

        var request = new CreateOrderRequest
        {
            CustomerId = customer.Id,
            Items =
            {
                new CreateOrderItemRequest
                {
                    ProductId = product.Id,
                    Quantity = quantity
                }
            }
        };

        var success = await _api.PostAsync("orders", request);
        if (!success)
        {
            MessageBox.Show("Unable to create order.");
            return;
        }

        QuantityTextBox.Text = "1";
        await RefreshAsync();
    }

    private async void ConfirmSelected_Click(object sender, RoutedEventArgs e)
    {
        if (OrdersGrid.SelectedItem is not Order order)
        {
            MessageBox.Show("Select an order to confirm.");
            return;
        }

        var success = await _api.PutAsync($"orders/{order.Id}/confirm");
        if (!success)
        {
            MessageBox.Show("Unable to confirm order. Check stock availability or order status.");
            return;
        }

        await RefreshAsync();
    }

    private async void DeleteSelected_Click(object sender, RoutedEventArgs e)
    {
        if (OrdersGrid.SelectedItem is not Order order)
        {
            MessageBox.Show("Select an order to delete.");
            return;
        }

        var success = await _api.DeleteAsync($"orders/{order.Id}");
        if (!success)
        {
            MessageBox.Show("Unable to delete order. Delete linked invoices first if any exist.");
            return;
        }

        await RefreshAsync();
    }
}