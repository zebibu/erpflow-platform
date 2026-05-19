using System.Windows;

namespace ERPFlowItalia.Desktop;

public partial class App : Application
{
	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		ShutdownMode = ShutdownMode.OnMainWindowClose;

		var authWindow = new AuthWindow();
		MainWindow = authWindow;
		authWindow.Show();
	}
}
