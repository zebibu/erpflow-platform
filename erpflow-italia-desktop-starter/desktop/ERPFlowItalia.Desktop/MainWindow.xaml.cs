using ERPFlowItalia.Desktop.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ERPFlowItalia.Desktop;

public partial class MainWindow : Window
{
    private readonly Brush _sidebarButtonForegroundBrush;
    private readonly Brush _sidebarButtonActiveForegroundBrush;
    private readonly Brush _sidebarButtonActiveBrush;
    private readonly Brush _sidebarButtonActiveBorderBrush;

    public MainWindow()
    {
        InitializeComponent();
        _sidebarButtonForegroundBrush = (Brush)FindResource("SidebarButtonForegroundBrush");
        _sidebarButtonActiveForegroundBrush = (Brush)FindResource("SidebarButtonActiveForegroundBrush");
        _sidebarButtonActiveBrush = (Brush)FindResource("SidebarButtonActiveBrush");
        _sidebarButtonActiveBorderBrush = (Brush)FindResource("SidebarButtonActiveBorderBrush");
        ShowDashboard();
    }

    private void SetPage(string title, string subtitle, object view, Button activeButton)
    {
        PageTitle.Text = title;
        PageSubtitle.Text = subtitle;
        MainContent.Content = view;
        SetActiveNavigation(activeButton);
    }

    private void SetActiveNavigation(Button activeButton)
    {
        foreach (var button in new[]
                 {
                     DashboardButton,
                     ProductsButton,
                     WarehousesButton,
                     StockButton,
                     OrdersButton,
                     CustomersButton,
                     SuppliersButton,
                     InvoicesButton,
                     ReportsButton
                 })
        {
            var isActive = button == activeButton;
            button.Background = isActive ? _sidebarButtonActiveBrush : Brushes.Transparent;
            button.BorderBrush = isActive ? _sidebarButtonActiveBorderBrush : Brushes.Transparent;
            button.Foreground = isActive ? _sidebarButtonActiveForegroundBrush : _sidebarButtonForegroundBrush;
        }
    }

    private void ShowDashboard()
    {
        SetPage(
            "Dashboard",
            "Overview of warehouse, stock, sales, invoices and company operations.",
            new DashboardView(),
            DashboardButton
        );
    }

    private void Dashboard_Click(object sender, RoutedEventArgs e) => ShowDashboard();

    private void Products_Click(object sender, RoutedEventArgs e)
    {
        SetPage("Products", "Manage products, SKUs, prices, VAT and minimum stock.", new ProductsView(), ProductsButton);
    }

    private void Warehouses_Click(object sender, RoutedEventArgs e)
    {
        SetPage("Warehouses", "Manage Italian company warehouse locations.", new WarehousesView(), WarehousesButton);
    }

    private void Stock_Click(object sender, RoutedEventArgs e)
    {
        SetPage("Stock", "Track current stock, low stock and stock movements.", new StockView(), StockButton);
    }

    private void Orders_Click(object sender, RoutedEventArgs e)
    {
        SetPage("Orders", "Create and confirm customer orders with stock validation.", new OrdersView(), OrdersButton);
    }

    private void Customers_Click(object sender, RoutedEventArgs e)
    {
        SetPage("Customers", "Manage customers, VAT numbers and company information.", new CustomersView(), CustomersButton);
    }

    private void Suppliers_Click(object sender, RoutedEventArgs e)
    {
        SetPage("Suppliers", "Manage suppliers and product sources.", new SuppliersView(), SuppliersButton);
    }

    private void Invoices_Click(object sender, RoutedEventArgs e)
    {
        SetPage("Invoices", "Generate and manage invoices from confirmed orders.", new InvoicesView(), InvoicesButton);
    }

    private void Reports_Click(object sender, RoutedEventArgs e)
    {
        SetPage("Reports", "View sales, stock, invoice and warehouse movement reports.", new ReportsView(), ReportsButton);
    }

    private void Refresh_Click(object sender, RoutedEventArgs e)
    {
        if (MainContent.Content is IRefreshable refreshable)
        {
            _ = refreshable.RefreshAsync();
        }
    }
}
