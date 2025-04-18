using System;
using CommunityToolkit.Mvvm.ComponentModel;
namespace CompClubGUI.Admin.API.Models
{
    /// <summary>
    /// Model for club
    /// </summary>
    public partial class ClubModel : BaseAdminModel
    {
        [ObservableProperty]
        private string address;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string phone;

        [ObservableProperty]
        private string workingHours;

        [ObservableProperty]
        private DateTime createdAt;

        [ObservableProperty]
        private bool isAlive;
    }
}
