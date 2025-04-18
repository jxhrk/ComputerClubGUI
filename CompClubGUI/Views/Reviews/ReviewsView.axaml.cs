using Avalonia.Controls;
using Avalonia.Interactivity;
using CompClubGUI.API.APIs;
using CompClubGUICore;
using CompClubGUICore.Views;

namespace CompClubGUI.Views;

public partial class ReviewsView : BaseMainAppView
{

    public ReviewsView() : base()
    {
        InitializeComponent();
        MainElement = MainGrid;
        Update();
    }
    
    public async void Update()
    {
        await ClubsApi.GetFeedbacks();
        FeedbacksList.ItemsSource = AppData.Feedbacks;
    }

    protected override UserControl? GetPreviousPanel()
    {
        return new MainView();
    }

    private void Button_Click(object? sender, RoutedEventArgs e)
    {
        AppActions.SwitchFrame(new LeaveReviewView());
    }
}