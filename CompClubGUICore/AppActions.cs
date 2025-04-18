using Avalonia.Controls;
using Avalonia.Styling;
using Avalonia;

namespace CompClubGUICore
{
    public class AppActions
    {
        public static void SwitchFrame(UserControl content)
        {
            if (Application.Current is AppBase app)
                app.SwitchFrame(content);
        }

        public static void SwitchTheme()
        {
            AppInfo.IsThemeDark = !AppInfo.IsThemeDark;
            if (Application.Current == null) { return; }
            Application.Current.RequestedThemeVariant = AppInfo.IsThemeDark ? ThemeVariant.Dark : ThemeVariant.Light;
        }
    }
}
