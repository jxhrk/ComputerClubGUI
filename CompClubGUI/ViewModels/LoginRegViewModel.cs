using CommunityToolkit.Mvvm.ComponentModel;

namespace CompClubGUI.ViewModels;

public partial class LoginRegViewModel : ViewModelBase
{
    public static string[] ActionButtonNames =
    [
        "Войти",
        "Зарегистрироваться",
        "Сбросить пароль",
    ];

    public static string[] SwitchStateNames =
    [
        "Регистрация",
        "Вход",
        "Вход",
    ];
}
