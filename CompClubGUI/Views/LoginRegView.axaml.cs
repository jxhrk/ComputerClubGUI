using System;
using System.Threading.Tasks;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using CompClubGUI.API.APIs;
using CompClubGUICore.API.Models;
using CompClubGUI.ViewModels;
using CompClubGUICore.Views.Scaling;
using CompClubGUICore;
using CompClubGUICore.Views;
using CompClubGUI.Views.Controls;
using System.Collections.Generic;
using CompClubGUICore.Views.Controls;

namespace CompClubGUI.Views;

enum State
{
    Login,
    Register,
    PasswordReset
}

public partial class LoginRegView : BaseMainAppView
{
    State state = State.Login;

    LoginRegViewModel ViewModel;

    Scalable<double>? loginPanelScalable;

    List<NavTextBox> textBoxes = [];

    public LoginRegView() : base()
    {
        InitializeComponent();
        MainElement = MainGrid;
        textBoxes = [LoginText, PasswordText, RepeatPasswordText, EmailText, FullNameText, EmailResetText];

        LoadingPanel.IsVisible = false;
        LoadingPanel.Opacity = 0;

        RegFields.IsVisible = false;
        RegFields.Opacity = 0;

        ResetFields.IsVisible = false;
        ResetFields.Opacity = 0;

        ViewModel = new LoginRegViewModel();
        DataContext = ViewModel;
    }

    protected override void OnInitialized(object? sender, EventArgs e)
    {
        base.OnInitialized(sender, e);
        loginPanelScalable = ScalingManager.GetScalable(LoginRegPanel, "MaxHeight") as Scalable<double>;
    }

    public async void ActionButton_Click(object sender, RoutedEventArgs e)
    {
        switch (state)
        {
            case State.Login:
                await LoginAction();
                break;
            case State.Register:
                await RegAction();
                break;
            case State.PasswordReset:
                await ResetAction();
                break;
        }
    }
    public void ThemeButton_Click(object sender, RoutedEventArgs e)
    {
        AppActions.SwitchTheme();
    }

    private static async void ShowHideElement(bool show, Control control, int hiddenDelay = 0)
    {
        double newOpacity = show ? 1 : 0;
        control.Opacity = newOpacity;
        control.IsHitTestVisible = show;
        control.Focusable = show;
        if (!show)
        {
            await Task.Delay(hiddenDelay);
        }
        if (show || control.Opacity == newOpacity)
        {
            control.IsVisible = show;
        }
    }

    public void StateChangeButton_Click(object sender, RoutedEventArgs e)
    {
        ChangeState(state == State.Login ? State.Register : State.Login);
    }

    private async void ChangeState(State newState)
    {
        state = newState;
        int duration = 200;
        LoginRegPanel.Transitions =
        [
            new DoubleTransition() { Duration = TimeSpan.FromMilliseconds(duration), Easing = new CubicEaseOut(), Property = Grid.MaxHeightProperty},
        ];

        double newHeight = state == State.Register ? 360 : 190;
        ScalingManager.SetScale(loginPanelScalable, newHeight);

        ShowHideElement(state != State.PasswordReset, LoginFields, duration);
        ShowHideElement(state == State.Register, RegFields, duration);
        ShowHideElement(state == State.PasswordReset, ResetFields, duration);

        int index = (int)state;
        ActionButton.Content = LoginRegViewModel.ActionButtonNames[index];
        StateChangeButton.Content = LoginRegViewModel.SwitchStateNames[index];
        ForgotPasswordButton.IsVisible = state == State.Login;

        await Task.Delay(duration);
        LoginRegPanel.Transitions = [];
    }

    public void ForgotPasswordButton_Click(object sender, RoutedEventArgs e)
    {
        ChangeState(state == State.PasswordReset ? State.Login : State.PasswordReset);
    }

    public async Task LoginAction()
    {
        if (!LoginText.ValidateAndGet(out string login) ||
            !PasswordText.ValidateAndGet(out string password))
        {
            return;
        }

        ShowLoading();

        int status = await AuthApi.AuthAsync(new AuthModel(login, password));

        if (status == 200 || AppInfo.AUTOLOGIN_DEBUG)
        {
            GlobalActions.UpdateUserInfo();
            AppActions.SwitchFrame(new MainView());
            return;
        }
        else if (status == 404)
        {
            LoginText.IndicateRed();
            PasswordText.IndicateRed();
        }

        HideLoading();
    }

    public async Task ResetAction()
    {
        string? email = EmailResetText.Text;

        if (string.IsNullOrEmpty(email)) return;

        ShowLoading();

        int status = await UsersApi.ResetPasswordAsync(email);

        if (status == 200 || AppInfo.AUTOLOGIN_DEBUG)
        {
            ChangeState(State.PasswordReset);
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
                textBox.Focusable = !show;
            ActionButton.Focusable = !show;
        });
    }


    public async Task RegAction()
    {
        if (!LoginText.ValidateAndGet(out string login) ||
            !PasswordText.ValidateAndGet(out string password) ||
            !RepeatPasswordText.ValidateAndGet(out string repeatPassword) ||
            !EmailText.ValidateAndGet(out string email) ||
            !FullNameText.ValidateAndGet(out string fullName))
            //parts == null || parts.Length < 2 ||
            //password != repeatPassword)
        {
            return;
        }

        if (password != repeatPassword)
        {
            PasswordText.IndicateRed();
            RepeatPasswordText.IndicateRed();
            return;
        }

        ShowLoading();

        string[] parts = fullName.Split(" ");
        string lastName = parts[0];
        string firstName = parts[1];
        string? MiddleName = parts.Length > 2 ? parts[2] : null;

        int status = await AuthApi.RegisterAsync(new RegisterModel(login, password, firstName, lastName, MiddleName, email));

        if (status != 201)
        {
            HideLoading();
            return;
        }

        await LoginAction();
    }
}