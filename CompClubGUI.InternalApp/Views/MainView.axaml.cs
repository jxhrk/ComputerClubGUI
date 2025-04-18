using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Avalonia.Automation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CompClubGUICore.API.Models;
using CompClubGUICore.Views;
using Tmds.DBus.Protocol;

namespace CompClubGUI.InternalApp.Views;

public partial class MainView : BaseMainAppView
{
    public MainView() : base()
    {
        InitializeComponent();
        MainElement = MainGrid;

        var missingPaths = paths.Where(o => !File.Exists(o.Value)).Select(o => o.Key).ToList();

        if (missingPaths.Any())
        {
            new MissingPathsWindow(missingPaths).Show();
        }
    }

    private Dictionary<string, string> paths = new Dictionary<string, string>()
    {
        { "Steam", @"C:\Windows\System32\notepad.exe" },
        { "Epic Games", @"C:\Windows\System32\calc.exe" },
        { "Epic Games2", @"C:\Windows\System32\calc2.exe" },
        { "Epic Games3", @"C:\Windows\System32\calc2342.exe" },
        { "Epic Games24", @"C:\Windows\System32\ca532lc322.exe" },
        { "Epic Games56", @"C:\Windows\System32\cal325c2.exe" },
    };

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        string name = (sender as Button).Content as string;

        if (!paths.TryGetValue(name, out string? path))
            return;

        if (!File.Exists(path)) return;
        Process.Start(path);
    }
}