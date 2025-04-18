using Avalonia.Controls;
using CompClubGUI.Views;
using CompClubGUICore;

namespace CompClubGUI.Desktop;

public partial class MainWindow : Window
{
    public static MainWindow? Instance { get; private set; }

    public MainWindow()
    {
        InitializeComponent();
        Instance = this;

        AppActions.SwitchFrame(new LoginRegView());
        //AppActions.SwitchFrame(new MainView());
        AppActions.SwitchTheme();
        //AppActions.SwitchFrame(new AdminView());

        AppInfo.ScalingFactorMultiplier = 1.3;
    }
}