using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CompClubGUI.API.APIs;
using CompClubGUICore.API.Models;
using CompClubGUICore.Views;

namespace CompClubGUI.Views;

public partial class LeaveReviewView : BaseMainAppView
{
    int Mark = 0;

    readonly List<Button> StarButtons;

    readonly BookingModel? CurrentBooking;

    public LeaveReviewView() : base()
    {
        InitializeComponent();
        MainElement = MainGrid;
        StarButtons = [ star1, star2, star3, star4, star5 ];
    }

    public LeaveReviewView(BookingModel booking) : this()
    {
        CurrentBooking = booking;
    }

    protected override UserControl? GetPreviousPanel()
    {
        return new MainView();
    }

    private void StarButton_Click(object? sender, RoutedEventArgs e)
    {
        Mark = StarButtons.FindIndex(o => o == sender) + 1;

        for (int i = 0; i < StarButtons.Count; i++)
        {
            StarButtons[i].Classes.Clear();
            if (i < Mark)
            {
                StarButtons[i].Classes.Add("Starred");
            }
        }
    }
    private async void SupplyButton_Click(object? sender, RoutedEventArgs e)
    {
        string text = ReviewText.Text ?? "";
        int clubId = CurrentBooking == null ? -1 : CurrentBooking.IdWorkingSpace;

        int status = await ClubsApi.SendFeedback(clubId, text, Mark);

        NavigateBack();
    }
}