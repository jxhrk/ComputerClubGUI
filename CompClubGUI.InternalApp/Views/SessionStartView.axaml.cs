using Avalonia.Interactivity;
using CompClubGUICore.Views;

namespace CompClubGUI.InternalApp.Views;

public partial class SessionStartView : BaseMainAppView
{
    public SessionStartView() : base()
    {
        InitializeComponent();
        MainElement = MainGrid;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        (App.Current as App)?.SwitchFrame(new MainView());
    }
}