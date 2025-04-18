using Avalonia.Controls;
using Avalonia.Interactivity;
using CompClubGUICore.Views;

namespace CompClubGUI;

public partial class BackButton : UserControl
{
    public BackButton()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (Parent != null && Parent.Parent is BaseMainAppView view)
        {
            view.NavigateBack();
        }
    }
}