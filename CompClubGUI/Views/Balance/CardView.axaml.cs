using Avalonia.Controls;
using Avalonia.Interactivity;
using CompClubGUI.API.APIs;
using CompClubGUICore;
using CompClubGUICore.Views;

namespace CompClubGUI.Views;

public partial class CardView : BaseMainAppView
{

    public CardView() : base()
    {
        InitializeComponent();
        MainElement = MainGrid;
    }

    public async void SaveCardButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(CardText.Text) || string.IsNullOrEmpty(CVVText.Text))
        {
            return;
        }
        string card = CardText.Text.Replace(" ", "");
        string cvv = CVVText.Text;
        if (card.Length != 16 || cvv.Length != 3)
        {
            return;
        }
        await PaymentsApi.SetPaymentInfoAsync(card, cvv);
        AppActions.SwitchFrame(new BalanceAddView());
    }

    protected override UserControl? GetPreviousPanel()
    {
        return new BalanceAddView();
    }

}