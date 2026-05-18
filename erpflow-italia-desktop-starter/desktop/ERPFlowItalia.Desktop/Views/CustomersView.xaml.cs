using ERPFlowItalia.Desktop.Models;
using ERPFlowItalia.Desktop.Services;
using System.Windows;
using System.Windows.Controls;

namespace ERPFlowItalia.Desktop.Views;

public partial class CustomersView : UserControl, IRefreshable
{
    private readonly ApiClient _api = new();
    private Customer? _selectedCustomer;

    public CustomersView()
    {
        InitializeComponent();
        CountryTextBox.Text = "Italy";
        _ = RefreshAsync();
    }

    public async Task RefreshAsync()
    {
        CustomersGrid.ItemsSource = await _api.GetListAsync<Customer>("customers");
    }

    private void CustomersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CustomersGrid.SelectedItem is not Customer customer)
        {
            return;
        }

        _selectedCustomer = customer;
        CompanyNameTextBox.Text = customer.CompanyName;
        VatNumberTextBox.Text = customer.VatNumber;
        FiscalCodeTextBox.Text = customer.FiscalCode;
        EmailTextBox.Text = customer.Email;
        PhoneTextBox.Text = customer.Phone;
        CityTextBox.Text = customer.City;
        ProvinceTextBox.Text = customer.Province;
        CountryTextBox.Text = customer.Country;
    }

    private async void Save_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CompanyNameTextBox.Text))
        {
            MessageBox.Show("Company name is required.");
            return;
        }

        var customer = new Customer
        {
            CompanyName = CompanyNameTextBox.Text.Trim(),
            VatNumber = VatNumberTextBox.Text.Trim(),
            FiscalCode = FiscalCodeTextBox.Text.Trim(),
            Email = EmailTextBox.Text.Trim(),
            Phone = PhoneTextBox.Text.Trim(),
            City = CityTextBox.Text.Trim(),
            Province = ProvinceTextBox.Text.Trim(),
            Country = string.IsNullOrWhiteSpace(CountryTextBox.Text) ? "Italy" : CountryTextBox.Text.Trim()
        };

        var success = _selectedCustomer is { Id: > 0 }
            ? await _api.PutAsync($"customers/{_selectedCustomer.Id}", customer)
            : await _api.PostAsync("customers", customer);

        if (!success)
        {
            MessageBox.Show("Unable to save customer.");
            return;
        }

        await RefreshAsync();
        ResetForm();
    }

    private async void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedCustomer is not { Id: > 0 })
        {
            MessageBox.Show("Select a customer to delete.");
            return;
        }

        var success = await _api.DeleteAsync($"customers/{_selectedCustomer.Id}");
        if (!success)
        {
            MessageBox.Show("Unable to delete customer. Delete related orders first if any exist.");
            return;
        }

        await RefreshAsync();
        ResetForm();
    }

    private void New_Click(object sender, RoutedEventArgs e)
    {
        ResetForm();
    }

    private void ResetForm()
    {
        _selectedCustomer = null;
        CustomersGrid.SelectedItem = null;
        CompanyNameTextBox.Clear();
        VatNumberTextBox.Clear();
        FiscalCodeTextBox.Clear();
        EmailTextBox.Clear();
        PhoneTextBox.Clear();
        CityTextBox.Clear();
        ProvinceTextBox.Clear();
        CountryTextBox.Text = "Italy";
    }
}