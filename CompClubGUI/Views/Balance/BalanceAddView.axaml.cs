using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CompClubGUI.API.APIs;
using CompClubGUICore;
using CompClubGUICore.Views;
using CompClubGUICore.API.Models;

namespace CompClubGUI.Views;

public partial class BalanceAddView : BaseMainAppView
{

    public BalanceAddView() : base()
    {
        InitializeComponent();
        MainElement = MainGrid;
        Update();
    }
    
    public async void Update()
    {
        if (AppInfo.IsLoggedIn())
        {
            PaymentModel? model = await PaymentsApi.GetPaymentInfoAsync();
            if (model == null)
            {
                CardText.Text = "Нет карты";
            }
            else if (model.CardNumber != null && model.CardNumber.Length == 16)
            {
                CardText.Text = $"*{model.CardNumber[^4..]}";
            }
        }
    }

    public void EditCardButton_Click(object sender, RoutedEventArgs e)
    {
        AppActions.SwitchFrame(new CardView());
    }

    private async void AddBalanceButton_Click(object? sender, RoutedEventArgs e)
    {
        string? money = PriceText.Text;
        await UsersApi.PutAccountBalance(Convert.ToDecimal(money));
        AppActions.SwitchFrame(new MainView());
        GlobalActions.UpdateUserInfo();
    }

    protected override UserControl? GetPreviousPanel()
    {
        return new BalanceView();
    }
}