using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using CompClubGUI.Admin.API.Models;

namespace CompClubGUI.Admin
{
    internal class AdminApp : App
    {
        public static int CurrentClubID = 1;

        public static List<EmployeeRoleModel> EmployeeRoles = [];

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                DisableAvaloniaDataAnnotationValidation();
                desktop.MainWindow = new MainWindow();
            }
            base.OnFrameworkInitializationCompleted();
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }

        public override void SwitchFrame(Control control)
        {
            if (MainWindow.Instance is not MainWindow window)
                return;

            window.ContentGrid.Children.Clear();
            window.ContentGrid.Children.Add(control);
            window.FrameContent = control;
        }
    }
}
