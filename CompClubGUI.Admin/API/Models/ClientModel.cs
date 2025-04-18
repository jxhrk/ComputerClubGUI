using System;
using CommunityToolkit.Mvvm.ComponentModel;
namespace CompClubGUI.Admin.API.Models
{
    /// <summary>
    /// Model for client
    /// </summary>
    public partial class ClientModel : BaseAdminModel
    {
        [ObservableProperty]
        private int idClient;

        [ObservableProperty]
        private decimal balance;

        [ObservableProperty]
        private string login;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private DateTime? lastLogin;

        [ObservableProperty]
        private DateTime? passwordChangedAt;

        [ObservableProperty]
        private DateTime createdAt;

        [ObservableProperty]
        private DateTime? updatedAt;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private ClientPersonalModel idClientNavigation;

        [ObservableProperty]
        private bool isAlive;
    }
}
