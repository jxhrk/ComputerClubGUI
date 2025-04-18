using System;
using CommunityToolkit.Mvvm.ComponentModel;
namespace CompClubGUI.Admin.API.Models
{
    /// <summary>
    /// Model for client
    /// </summary>
    public partial class ClientPersonalModel : BaseAdminModel
    {
        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string middleName;
        
        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private DateTime createdAt;

        [ObservableProperty]
        private DateTime? updatedAt;
    }
}
