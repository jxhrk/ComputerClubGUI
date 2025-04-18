using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;

namespace CompClubGUI.InternalApp;

public partial class MissingPathsWindow : Window
{
    public MissingPathsWindow()
    {
        InitializeComponent();
    }

    public MissingPathsWindow(List<string> names) : this()
    {
        AppsList.ItemsSource = names;
    }
}