using Avalonia.Controls;
using CompClubGUI.Admin.Views;
using CompClubGUICore;

namespace CompClubGUI.Admin
{
    public partial class MainWindow : Window
    {
        public static MainWindow? Instance { get; private set; }

        public Control FrameContent;

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;

            AppActions.SwitchFrame(new LoginRegView());
            AppActions.SwitchTheme();
        }
    }
}