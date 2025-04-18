using Avalonia.Controls;
using Avalonia.Interactivity;
using CompClubGUI.API.APIs;
using CompClubGUICore;
using CompClubGUICore.Views;

namespace CompClubGUI.Views;

public partial class BalanceView : BaseMainAppView
{

    public BalanceView() : base()
    {
        InitializeComponent();
        if (AppInfo.IsLoggedIn())
        { 
            RefreshBalanceHistory();
        }
    }
    public async void RefreshBalanceHistory()
    {
        await UsersApi.GetBalanceHistory();
        //List<BalanceHistoryModel> history = new();
        //for (int i = 0; i < 10; i++)
        //{
        //    history.Add(new BalanceHistoryModel()
        //    {
        //        Balance = Random.Shared.Next(-500, 500),
        //        Date = new DateTime(2025, Random.Shared.Next(1, 12), Random.Shared.Next(1,28))
        //    });
        //}

        NoBalanceHistory.IsVisible = AppData.BalanceHistory == null || AppData.BalanceHistory.Count == 0;

        BalanceHistoryList.ItemsSource = AppData.BalanceHistory;
        //BalanceHistoryList.ItemsSource = history;
    }

    public void AddPaymentButton_Click(object sender, RoutedEventArgs e)
    {
        AppActions.SwitchFrame(new BalanceAddView());
    }

    protected override UserControl? GetPreviousPanel()
    {
        return new MainView();
    }

}