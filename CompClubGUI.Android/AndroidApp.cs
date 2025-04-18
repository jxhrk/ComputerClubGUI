using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Platform;
using Avalonia.Interactivity;
using CompClubGUI.ViewModels;
using CompClubGUI.Views;
using CompClubGUICore;
using CompClubGUICore.Views;

namespace CompClubGUI.Android
{
    public partial class AndroidApp : App
    {
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new LoginRegView
                {
                    DataContext = new LoginRegViewModel()
                };
                singleViewPlatform.MainView.Loaded += MainView_Loaded;

                AppInfo.ScalingFactorMultiplier = 1;
            }
            base.OnFrameworkInitializationCompleted();
        }

        private void MainView_Loaded(object? sender, RoutedEventArgs e)
        {
            TopLevel? top = TopLevel.GetTopLevel(sender as Control);
            if (top == null) { return; }
            top.BackRequested += Top_BackRequested;
            var inputPane = top.InputPane;
            if (inputPane == null) { return; }
            inputPane.StateChanged += InputPane_StateChanged;
        }

        private void Top_BackRequested(object? sender, RoutedEventArgs e)
        {
            var mainView = GetMainView();
            if (mainView != null)
            {
                mainView.NavigateBack();
            }
            e.Handled = true;
        }

        private void InputPane_StateChanged(object? sender, InputPaneStateEventArgs e)
        {
            var mainView = GetMainView();
            if (mainView != null)
            {
                if (mainView.MainElement != null)
                {
                    mainView.MainElement.Margin = new Thickness(0, 0, 0, e.EndRect.Height);
                }
            }
        }

        public override void SwitchFrame(Control control)
        {
            var singleView = GetSingleViewPlatform();
            if (singleView != null)
            {
                singleView.MainView = control;
                return;
            }
        }

        private BaseMainAppView? GetMainView()
        {
            var singleView = GetSingleViewPlatform();
            if (singleView != null)
            {
                return singleView.MainView as BaseMainAppView;
            }
            return null;
        }

        private static ISingleViewApplicationLifetime? GetSingleViewPlatform()
        {
            Application? app = Current;
            if (app != null && app.ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                return singleViewPlatform;
            }
            return null;
        }
    }
}
