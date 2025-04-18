using Avalonia.Markup.Xaml;
using CompClubGUICore;

namespace CompClubGUI;

public partial class App : AppBase
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
}