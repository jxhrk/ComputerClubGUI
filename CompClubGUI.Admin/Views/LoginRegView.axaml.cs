using System;
using System.Threading.Tasks;
using Avalonia.Interactivity;
using Avalonia.Threading;
using CompClubGUICore.API.Models;
using CompClubGUICore;
using CompClubGUICore.Views;
using CompClubGUI.Admin.API.APIs;

namespace CompClubGUI.Admin.Views;

public partial class LoginRegView : BaseMainAppView
{

    public LoginRegView() : base()
    {
        InitializeComponent();
        MainElement = MainGrid;

        LoadingPanel.IsVisible = false;
        LoadingPanel.Opacity = 0;

        //ViewModel = DataContext as LoginRegViewModel;
    }

    protected override void OnInitialized(object? sender, EventArgs e)
    {
        base.OnInitialized(sender, e);
    }

    public async void ActionButton_Click(object sender, RoutedEventArgs e)
    {
        await LoginAction();
    }
    public void ThemeButton_Click(object sender, RoutedEventArgs e)
    {
        AppActions.SwitchTheme();
    }

    public async Task LoginAction()
    {
        if (!LoginText.ValidateAndGet(out string login) ||
            !PasswordText.ValidateAndGet(out string password))
        {
            return;
        }

        ShowLoading();

        AuthModel auth = new AuthModel(login, password);
        int status = await AdminAuthApi.AuthAsync(auth);

        if (status == 200 || AppInfo.AUTOLOGIN_DEBUG)
        {
            AppActions.SwitchFrame(new AdminView());
            return;
        }
        else if (status == 404)
        {
            LoginText.IndicateRed();
            PasswordText.IndicateRed();
        }

        HideLoading();
    }

    private void ShowLoading()
    {
        HideShowLoading(true);
    }

    private void HideLoading()
    {
        HideShowLoading(false);
    }

    private void HideShowLoading(bool show)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            ActionButton.IsEnabled = !show;
            LoadingPanel.IsVisible = show;
            LoadingPanel.Opacity = show ? 0.8 : 0;
        });
    }
}