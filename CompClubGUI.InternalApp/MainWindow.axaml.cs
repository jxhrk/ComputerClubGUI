using Avalonia;
using Avalonia.Controls;
using CompClubGUI.InternalApp.Views;

namespace CompClubGUI.InternalApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            (App.Current as App).Window = this;

            (App.Current as App)?.SwitchFrame(new LoginRegView());
        }
    }
}