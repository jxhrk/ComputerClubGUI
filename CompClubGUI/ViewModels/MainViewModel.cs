using CommunityToolkit.Mvvm.ComponentModel;
using CompClubGUICore.API.Models;

namespace CompClubGUI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";

    [ObservableProperty]
    public UserModel _user;

    [ObservableProperty]
    private BookingModel? _currentBooking = null;
}
