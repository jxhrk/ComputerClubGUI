using Avalonia.Controls;
using Avalonia.Interactivity;
using CompClubGUI.Admin.API.APIs;
using CompClubGUI.Admin.API.Models;
using CompClubGUI.Admin.Views.Controls;
using System.Threading.Tasks;

namespace CompClubGUI.Admin;

public partial class EquipmentControl : DataFrameListElement<EquipmentModel>
{
    public EquipmentControl() : base()
    {
        InitializeComponent();

        //RolesComboBox.ItemsSource = AdminApp.EmployeeRoles;
    }

    override protected async Task<int> CallCreate(EquipmentModel model) => await EquipmentApi.CreateEquipment(model);
    override protected async Task<int> CallUpdate(EquipmentModel model) => await EquipmentApi.UpdateEquipment(model);
    override protected async Task<int> CallDelete(EquipmentModel model) => await EquipmentApi.DeleteEquipment(model.Id);

    private async void SaveButton_Click(object sender, RoutedEventArgs e) => Save();

    private async void DeleteButton_Click(object sender, RoutedEventArgs e) => Delete();
}