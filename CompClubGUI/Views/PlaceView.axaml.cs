using Avalonia.Controls;
using Avalonia.Interactivity;
using CompClubGUI.API.APIs;
using CompClubGUICore.API.Models;
using CompClubGUICore;
using CompClubGUICore.Views;

namespace CompClubGUI.Views;

public partial class PlaceView : BaseMainAppView
{

    public PlaceView() : base()
    {
        InitializeComponent();
        MainElement = MainGrid;
    }

    public PlaceView(BookingModel model) : this()
    {
        DataContext = model;
    }

    private async void CancelBookingButton_Click(object? sender, RoutedEventArgs e)
    {
        await ClubsApi.CancelBooking();
        AppActions.SwitchFrame(new MainView());
        GlobalActions.UpdateUserInfo();
    }

    private void LeaveFeedbackButton_Click(object? sender, RoutedEventArgs e)
    {
        AppActions.SwitchFrame(new LeaveReviewView(DataContext as BookingModel));
        GlobalActions.UpdateUserInfo();
    }

    protected override UserControl? GetPreviousPanel()
    {
        return new MainView();
    }
}