using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CompClubGUI.API.APIs;
using CompClubGUICore;
using CompClubGUICore.Views;
using CompClubGUICore.API.Models;

namespace CompClubGUI.Views;

public partial class BuyNewPlaceView : BaseMainAppView
{

    public BuyNewPlaceView() : base()
    {
        InitializeComponent();
        MainElement = MainGrid;
        Update();

        DateMonthComboBox.SelectedIndex = DateTime.Now.Month - 1;
        DateDayComboBox.SelectedIndex = DateTime.Now.Day - 1;
    }

    public async void Update()
    {
        if (AppData.Clubs == null)
        {
            await ClubsApi.GetClubs();
        }

        if (AppData.Clubs != null)
        {
            ClubsComboBox.ItemsSource = AppData.Clubs;
        }

        ClubsComboBox.ItemsSource = AppData.Clubs;

        List<string> places = [];
        for (int i = 1; i <= 15; i++)
        {
            places.Add($"Место {i}");
        }
        PlacesList.ItemsSource = places;

        List<string> hours = [];
        for (int i = 0; i < 24; i++)
        {
            hours.Add($"{i}");
        }
        TimeHourComboBox.ItemsSource = hours;

        List<string> minutes = [];
        for (int i = 0; i < 60; i+=5)
        {
            minutes.Add(i.ToString("00"));
        }
        TimeMinuteComboBox.ItemsSource = minutes;
    }

    public void UpdateDateComboBox()
    {
        int monthId = DateMonthComboBox.SelectedIndex;
        if (monthId == -1) return;
        int count = DateTime.DaysInMonth(DateTime.Now.Year, monthId + 1);
        List<string> items = [];
        for (int i = 1; i <= count; i++)
        {
            items.Add(i.ToString());
        }

        if (DateDayComboBox.SelectedIndex + 1 > items.Count)
        {
            DateDayComboBox.SelectedIndex = 0;
        }

        DateDayComboBox.ItemsSource = items;
    }

    private async void UpdatePlacesList(int clubId)
    {
        // TODO: remove super secret admin data from response
        if (await ClubsApi.GetPlacesInClub(clubId) == 200)
        {
            PlacesList.ItemsSource = AppData.CurrentPlaces_TEMP;
        }
    }

    private async void ApplyButton_Click(object? sender, RoutedEventArgs e)
    {
        DateTime date = new
        (
            DateTime.Now.Year,
            DateMonthComboBox.SelectedIndex + 1,
            DateDayComboBox.SelectedIndex + 1,
            TimeHourComboBox.SelectedIndex,
            Convert.ToInt32(TimeMinuteComboBox.SelectedValue),
            0
        );

        if (PlacesList.SelectedItem is not PlaceModel place) return;

        int status = await ClubsApi.CreateBooking(place.ID, date, DateTime.MaxValue);

        AppActions.SwitchFrame(new MainView());
        GlobalActions.UpdateUserInfo();
    }

    protected override UserControl? GetPreviousPanel()
    {
        return new MainView();
    }

    public void ClubsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdatePlacesList((ClubsComboBox.SelectedItem as ClubModel).ID);
    }
    
    public void DateMonthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateDateComboBox();
        // UPDATE FILTER
    }
    public void TimeMinuteComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // UPDATE FILTER
    }
}