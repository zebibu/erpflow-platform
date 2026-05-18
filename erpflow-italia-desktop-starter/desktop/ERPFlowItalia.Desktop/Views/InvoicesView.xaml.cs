using ERPFlowItalia.Desktop.Models;
using ERPFlowItalia.Desktop.Services;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ERPFlowItalia.Desktop.Views;

public partial class InvoicesView : UserControl, IRefreshable
{
    private readonly ApiClient _api = new();

    public InvoicesView()
    {
        InitializeComponent();
        PaymentStatusComboBox.ItemsSource = Enum.GetValues(typeof(PaymentStatus));
        _ = RefreshAsync();
    }

    public async Task RefreshAsync()
    {
        var ordersTask = _api.GetListAsync<Order>("orders");
        var invoicesTask = _api.GetListAsync<Invoice>("invoices");

        await Task.WhenAll(ordersTask, invoicesTask);

        var orders = await ordersTask;
        var invoices = await invoicesTask;

        InvoicesGrid.ItemsSource = invoices;
        OrderComboBox.ItemsSource = orders
            .Where(order => invoices.All(invoice => invoice.OrderId != order.Id))
            .OrderByDescending(order => order.OrderDate)
            .ToList();
    }

    private void InvoicesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (InvoicesGrid.SelectedItem is Invoice invoice)
        {
            PaymentStatusComboBox.SelectedItem = invoice.PaymentStatus;
        }
    }

    private async void CreateInvoice_Click(object sender, RoutedEventArgs e)
    {
        if (OrderComboBox.SelectedItem is not Order order)
        {
            MessageBox.Show("Select an order first.");
            return;
        }

        var success = await _api.PostAsync($"invoices/from-order/{order.Id}");
        if (!success)
        {
            MessageBox.Show("Unable to create invoice for the selected order.");
            return;
        }

        await RefreshAsync();
    }

    private async void SaveStatus_Click(object sender, RoutedEventArgs e)
    {
        if (InvoicesGrid.SelectedItem is not Invoice invoice)
        {
            MessageBox.Show("Select an invoice to update.");
            return;
        }

        if (PaymentStatusComboBox.SelectedItem is not PaymentStatus paymentStatus)
        {
            MessageBox.Show("Select a payment status.");
            return;
        }

        var success = await _api.PutAsync($"invoices/{invoice.Id}/payment-status", paymentStatus);
        if (!success)
        {
            MessageBox.Show("Unable to update invoice status.");
            return;
        }

        await RefreshAsync();
    }

    private async void DeleteSelected_Click(object sender, RoutedEventArgs e)
    {
        if (InvoicesGrid.SelectedItem is not Invoice invoice)
        {
            MessageBox.Show("Select an invoice to delete.");
            return;
        }

        var success = await _api.DeleteAsync($"invoices/{invoice.Id}");
        if (!success)
        {
            MessageBox.Show("Unable to delete invoice.");
            return;
        }

        await RefreshAsync();
    }
}