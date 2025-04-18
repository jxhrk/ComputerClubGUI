using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace CompClubGUI.InternalApp
{
    public partial class App : Application
    {
        public MainWindow Window;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }

        public void SwitchFrame(Control control)
        {
            Window.ContentGrid.Children.Clear();
            Window.ContentGrid.Children.Add(control);
        }
    }
}