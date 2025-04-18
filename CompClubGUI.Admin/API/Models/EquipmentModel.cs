using System;
using CommunityToolkit.Mvvm.ComponentModel;
namespace CompClubGUI.Admin.API.Models
{
    /// <summary>
    /// Model for playable gaming place in club (including admin-only info)
    /// </summary>
    public partial class EquipmentModel : BaseAdminModel
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string type;

        [ObservableProperty]
        private string specification;

        [ObservableProperty]
        private double purchasePrice;

        [ObservableProperty]
        private DateTime purchaseDate;

        [ObservableProperty]
        private int idClub;

        [ObservableProperty]
        private int status;

        [ObservableProperty]
        private int quantity;
    }
}
