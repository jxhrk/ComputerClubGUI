using Avalonia.Controls;
using Avalonia.Interactivity;
using CompClubGUICore.Views.Scaling;
using CompClubGUICore;
using CompClubGUICore.Views;

namespace CompClubGUI.Views;

public partial class SettingsView : BaseMainAppView
{

    public SettingsView() : base()
    {
        InitializeComponent();
        MainElement = MainGrid;
        Update();
        DataContext = AppData.MainModel;
    }
    
    public void Update()
    {
        ThemeCheckBox.IsChecked = AppInfo.IsThemeDark;
        ScalingSlider.ParentView = this;
    }

    private void ApplyButton_Click(object? sender, RoutedEventArgs e)
    {
        AppActions.SwitchFrame(new MainView());
        GlobalActions.UpdateUserInfo();
    }

    protected override UserControl? GetPreviousPanel()
    {
        return new MainView();
    }

    private void CheckBox_IsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (ThemeCheckBox.IsChecked != AppInfo.IsThemeDark)
        {
            AppActions.SwitchTheme();
        }
    }

    public void UpdateZoom(double newZoom)
    {
        AppInfo.ScalingFactorMultiplier = newZoom;
        ScalingManager.SetScaling(currentFactor * AppInfo.ScalingFactorMultiplier);
    }
}