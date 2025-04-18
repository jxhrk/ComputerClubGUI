using Avalonia.Controls;
using Avalonia.Interactivity;
using CompClubGUI.API.APIs;
using CompClubGUICore.API.Models;
using CompClubGUICore;
using CompClubGUICore.Views;

namespace CompClubGUI.Views;

public partial class BookingsHistoryView : BaseMainAppView
{

    public BookingsHistoryView() : base()
    {
        InitializeComponent();
        MainElement = MainGrid;
        Update();
    }

    public async void Update()
    {
        await ClubsApi.GetBookings();

        BookingList.ItemsSource = AppData.Bookings;

        NoPlaces.IsVisible = AppData.Bookings == null || AppData.Bookings.Count == 0;
    }

    public void BuyPlaceButton_Click(object sender, RoutedEventArgs e)
    {
        AppActions.SwitchFrame(new BuyNewPlaceView());
    }

    public void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        AppActions.SwitchFrame(new PlaceView(BookingList.SelectedItem as BookingModel));
    }

    protected override UserControl? GetPreviousPanel()
    {
        return new MainView();
    }
}