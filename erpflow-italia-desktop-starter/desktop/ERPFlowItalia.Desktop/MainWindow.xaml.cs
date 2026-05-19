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
        SetPage("Products", "Catalogo articoli, listini, IVA e soglie minime per la distribuzione sul territorio italiano.", new ProductsView(), ProductsButton);
    }

    private void Warehouses_Click(object sender, RoutedEventArgs e)
    {
        SetPage("Warehouses", "Presidi logistici, indirizzi operativi e hub tra Nord, Centro e Sud Italia.", new WarehousesView(), WarehousesButton);
    }

    private void Stock_Click(object sender, RoutedEventArgs e)
    {
        SetPage("Stock", "Giacenze, sotto scorta e disponibilita per magazzino con priorita operative.", new StockView(), StockButton);
    }

    private void Orders_Click(object sender, RoutedEventArgs e)
    {
        SetPage("Orders", "Ordini clienti B2B con verifica disponibilita, imponibile, IVA e conferma operativa.", new OrdersView(), OrdersButton);
    }

    private void Customers_Click(object sender, RoutedEventArgs e)
    {
        SetPage("Customers", "Anagrafiche clienti con Partita IVA, Codice Fiscale, provincia, CAP e contatti amministrativi.", new CustomersView(), CustomersButton);
    }

    private void Suppliers_Click(object sender, RoutedEventArgs e)
    {
        SetPage("Suppliers", "Fornitori, filiera di approvvigionamento e riferimenti per acquisti sul mercato italiano ed EU.", new SuppliersView(), SuppliersButton);
    }

    private void Invoices_Click(object sender, RoutedEventArgs e)
    {
        SetPage("Invoices", "Fatture da ordini confermati con stato pagamento e controllo documentale.", new InvoicesView(), InvoicesButton);
    }

    private void Reports_Click(object sender, RoutedEventArgs e)
    {
        SetPage("Reports", "Valore stock, performance commerciali e visibilita operativa per direzione e logistica.", new ReportsView(), ReportsButton);
    }

    private void Refresh_Click(object sender, RoutedEventArgs e)
    {
        if (MainContent.Content is IRefreshable refreshable)
        {
            _ = refreshable.RefreshAsync();
        }
    }
}
