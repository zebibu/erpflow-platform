using ERPFlowItalia.Desktop.Models;
using ERPFlowItalia.Desktop.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ERPFlowItalia.Desktop;

public partial class AuthWindow : Window
{
    private readonly ApiClient _apiClient = new();
    private readonly Brush _successBrush;
    private readonly Brush _errorBrush;
    private readonly Brush _neutralBrush;

    public AuthWindow()
    {
        InitializeComponent();
        _successBrush = (Brush)FindResource("SuccessBrush");
        _errorBrush = (Brush)FindResource("AccentStrongBrush");
        _neutralBrush = (Brush)FindResource("TextSecondaryBrush");
        ShowSignIn();
    }

    private void ShowSignIn_Click(object sender, RoutedEventArgs e) => ShowSignIn();

    private void ShowSignUp_Click(object sender, RoutedEventArgs e) => ShowSignUp();

    private void ShowSignIn()
    {
        SignInPanel.Visibility = Visibility.Visible;
        SignUpPanel.Visibility = Visibility.Collapsed;
        SignInTabButton.Style = (Style)FindResource("PrimaryActionButtonStyle");
        SignUpTabButton.Style = (Style)FindResource("SecondaryActionButtonStyle");
    }

    private void ShowSignUp()
    {
        SignInPanel.Visibility = Visibility.Collapsed;
        SignUpPanel.Visibility = Visibility.Visible;
        SignInTabButton.Style = (Style)FindResource("SecondaryActionButtonStyle");
        SignUpTabButton.Style = (Style)FindResource("PrimaryActionButtonStyle");
    }

    private async void SignIn_Click(object sender, RoutedEventArgs e)
    {
        LoginStatusText.Foreground = _neutralBrush;
        LoginStatusText.Text = "Verifica credenziali in corso...";

        var response = await _apiClient.PostForResponseAsync<LoginRequest, AuthResponse>("auth/login", new LoginRequest
        {
            Email = LoginEmailTextBox.Text.Trim(),
            Password = LoginPasswordBox.Password
        });

        if (response?.Success == true)
        {
            LoginStatusText.Foreground = _successBrush;
            LoginStatusText.Text = response.Message;

            var mainWindow = new MainWindow(new AuthSession
            {
                FullName = response.FullName,
                Email = response.Email,
                Role = response.Role,
                Token = response.Token
            });

            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            Close();
            return;
        }

        LoginStatusText.Foreground = _errorBrush;
        LoginStatusText.Text = response?.Message ?? "Impossibile contattare il servizio di autenticazione.";
    }

    private async void SignUp_Click(object sender, RoutedEventArgs e)
    {
        if (RegisterPasswordBox.Password != ConfirmPasswordBox.Password)
        {
            RegisterStatusText.Foreground = _errorBrush;
            RegisterStatusText.Text = "Le password non coincidono.";
            return;
        }

        RegisterStatusText.Foreground = _neutralBrush;
        RegisterStatusText.Text = "Creazione account in corso...";

        var selectedRole = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "SalesTeam";
        var response = await _apiClient.PostForResponseAsync<RegisterRequest, AuthResponse>("auth/register", new RegisterRequest
        {
            FullName = RegisterFullNameTextBox.Text.Trim(),
            Email = RegisterEmailTextBox.Text.Trim(),
            Password = RegisterPasswordBox.Password,
            Role = selectedRole
        });

        if (response?.Success == true)
        {
            RegisterStatusText.Foreground = _successBrush;
            RegisterStatusText.Text = response.Message;
            LoginEmailTextBox.Text = RegisterEmailTextBox.Text.Trim();
            LoginStatusText.Foreground = _successBrush;
            LoginStatusText.Text = "Account creato. Accedi con le nuove credenziali.";
            ShowSignIn();
            return;
        }

        RegisterStatusText.Foreground = _errorBrush;
        RegisterStatusText.Text = response?.Message ?? "Impossibile completare la registrazione.";
    }
}