using ERPFlowItalia.Desktop.Models;
using ERPFlowItalia.Desktop.Services;
using System.Windows;
using System.Windows.Controls;

namespace ERPFlowItalia.Desktop.Views;

public partial class SuppliersView : UserControl, IRefreshable
{
    private readonly ApiClient _api = new();
    private Supplier? _selectedSupplier;

    public SuppliersView()
    {
        InitializeComponent();
        CountryTextBox.Text = "Italy";
        _ = RefreshAsync();
    }

    public async Task RefreshAsync()
    {
        SuppliersGrid.ItemsSource = await _api.GetListAsync<Supplier>("suppliers");
    }

    private void SuppliersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SuppliersGrid.SelectedItem is not Supplier supplier)
        {
            return;
        }

        _selectedSupplier = supplier;
        CompanyNameTextBox.Text = supplier.CompanyName;
        VatNumberTextBox.Text = supplier.VatNumber;
        EmailTextBox.Text = supplier.Email;
        PhoneTextBox.Text = supplier.Phone;
        CityTextBox.Text = supplier.City;
        CountryTextBox.Text = supplier.Country;
    }

    private async void Save_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CompanyNameTextBox.Text))
        {
            MessageBox.Show("Company name is required.");
            return;
        }

        var supplier = new Supplier
        {
            CompanyName = CompanyNameTextBox.Text.Trim(),
            VatNumber = VatNumberTextBox.Text.Trim(),
            Email = EmailTextBox.Text.Trim(),
            Phone = PhoneTextBox.Text.Trim(),
            City = CityTextBox.Text.Trim(),
            Country = string.IsNullOrWhiteSpace(CountryTextBox.Text) ? "Italy" : CountryTextBox.Text.Trim()
        };

        var success = _selectedSupplier is { Id: > 0 }
            ? await _api.PutAsync($"suppliers/{_selectedSupplier.Id}", supplier)
            : await _api.PostAsync("suppliers", supplier);

        if (!success)
        {
            MessageBox.Show("Unable to save supplier.");
            return;
        }

        await RefreshAsync();
        ResetForm();
    }

    private async void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedSupplier is not { Id: > 0 })
        {
            MessageBox.Show("Select a supplier to delete.");
            return;
        }

        var success = await _api.DeleteAsync($"suppliers/{_selectedSupplier.Id}");
        if (!success)
        {
            MessageBox.Show("Unable to delete supplier. Remove linked products first if any exist.");
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
        _selectedSupplier = null;
        SuppliersGrid.SelectedItem = null;
        CompanyNameTextBox.Clear();
        VatNumberTextBox.Clear();
        EmailTextBox.Clear();
        PhoneTextBox.Clear();
        CityTextBox.Clear();
        CountryTextBox.Text = "Italy";
    }
}