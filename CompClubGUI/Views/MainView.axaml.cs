using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Input;
using Avalonia.Interactivity;
using CompClubGUI.API.APIs;
using CompClubGUICore.API.Models;
using CompClubGUI.ViewModels;
using CompClubGUICore;
using CompClubGUICore.Views;

namespace CompClubGUI.Views;

public partial class MainView : BaseMainAppView
{
    MainViewModel viewModel;

    public MainView() : base()
    {
        InitializeComponent();
        MainElement = MainGrid;

        viewModel = AppData.MainModel;
        DataContext = viewModel;

        CurrentBookingDisplay.IsVisible = false;

        Update();
    }

    public async void Update()
    {
        await ClubsApi.GetBookings();
        if (AppData.Bookings == null && AppInfo.AUTOLOGIN_DEBUG)
        {
            GenerateDebugData();
        }

        viewModel.CurrentBooking = AppData.Bookings?.FirstOrDefault();

        NoPlaces.IsVisible = AppData.Bookings == null || AppData.Bookings.Count == 0;
        CurrentBookingDisplay.IsVisible = !NoPlaces.IsVisible;
    }

    private void GenerateDebugData()
    {
        DateTime startDate = DateTime.Now - TimeSpan.FromHours(1);

        const int count = 1;

        List<BookingModel> bookings = [];
        for (int i = 0; i < count; i++)
        {
            bookings.Add(new BookingModel()
            {
                StartTime = startDate + TimeSpan.FromMinutes(15 * i),
            });
        }

        bookings = bookings.OrderByDescending(o => o.StartTime).ToList();
        AppData.Bookings = bookings;
    }

    public void AddBalanceButton_Click(object sender, RoutedEventArgs e)
    {
        AppActions.SwitchFrame(new BalanceView());
    }

    public void BuyPlaceButton_Click(object sender, RoutedEventArgs e)
    {
        AppActions.SwitchFrame(new BuyNewPlaceView());
    }
    public void BookingsHustoryButton_Click(object sender, RoutedEventArgs e)
    {
        AppActions.SwitchFrame(new BookingsHistoryView());
    }

    public void CurrentBooking_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        AppActions.SwitchFrame(new PlaceView(viewModel.CurrentBooking));
    }
}