using CommunityToolkit.Mvvm.ComponentModel;
namespace CompClubGUI.Admin.API.Models
{
    /// <summary>
    /// Model for playable gaming place in club (including admin-only info)
    /// </summary>
    public partial class TariffModel : BaseAdminModel
    {
        [ObservableProperty]
        private string name;
        
        [ObservableProperty]
        private double pricePerMinute;
    }
}
