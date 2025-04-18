using System.Threading.Tasks;
using Avalonia.Interactivity;
using Avalonia.Threading;
using CompClubGUICore.API.Models;
using CompClubGUICore;
using CompClubGUICore.Views;
using System.Collections.Generic;
using CompClubGUICore.Views.Controls;
using CompClubGUI.InternalApp.API.APIs;

namespace CompClubGUI.InternalApp.Views;

public partial class LoginRegView : BaseMainAppView
{
    List<NavTextBox> textBoxes = [];

    public LoginRegView() : base()
    {
        InitializeComponent();
        MainElement = MainGrid;
        textBoxes = [LoginText, PasswordText];

        LoadingPanel.IsVisible = false;
        LoadingPanel.Opacity = 0;
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
        int status = await AuthApi.AuthAsync(auth);
        
        if (status == 200 || AppInfo.AUTOLOGIN_DEBUG)
        {
            (App.Current as App)?.SwitchFrame(new SessionStartView());
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
            foreach (NavTextBox textBox in textBoxes)
            {
                textBox.Focusable = !show;
            }
            ActionButton.Focusable = !show;
        });
    }
}