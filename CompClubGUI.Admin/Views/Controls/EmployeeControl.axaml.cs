using Avalonia.Controls;
using Avalonia.Interactivity;
using CompClubGUI.Admin.API.APIs;
using CompClubGUI.Admin.API.Models;
using CompClubGUI.Admin.Views.Controls;
using System.Threading.Tasks;

namespace CompClubGUI.Admin;

public partial class EmployeeControl : DataFrameListElement<EmployeeModel>
{
    public EmployeeControl() : base()
    {
        InitializeComponent();

        RolesComboBox.ItemsSource = AdminApp.EmployeeRoles;
    }

    override protected async Task<int> CallCreate(EmployeeModel model) => await EmployeesApi.CreateEmployee(model);
    override protected async Task<int> CallUpdate(EmployeeModel model) => await EmployeesApi.UpdateEmployee(model);
    override protected async Task<int> CallDelete(EmployeeModel model) => await EmployeesApi.DeleteEmployee(model.Id);

    private async void SaveButton_Click(object sender, RoutedEventArgs e) => Save();

    private async void DeleteButton_Click(object sender, RoutedEventArgs e) => Delete();
}