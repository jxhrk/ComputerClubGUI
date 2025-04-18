using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
namespace CompClubGUI.Admin.API.Models
{
    /// <summary>
    /// Model for employee
    /// </summary>
    public partial class EmployeeRoleModel : BaseAdminModel
    {
        [ObservableProperty]
        private string name;

        private static Dictionary<string, string> Names = new Dictionary<string, string>()
        {
            {"Owner", "Владелец"},
            {"Admin", "Админ"},
            {"Salesperson", "Продажник"}
        };

        public override string ToString() => Names.GetValueOrDefault(Name);

    }
}
