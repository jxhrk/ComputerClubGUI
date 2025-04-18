using Avalonia.Interactivity;
using CompClubGUI.Admin.API.APIs;
using CompClubGUI.Admin.API.Models;
using CompClubGUI.Admin.Views.Controls;
using System.Threading.Tasks;

namespace CompClubGUI.Admin;

public partial class ClientControl : DataFrameListElement<ClientModel>
{
    public ClientControl() : base()
    {
        InitializeComponent();
    }

    override protected async Task<int> CallCreate(ClientModel model) => await ClientsApi.CreateClient(model);
    override protected async Task<int> CallUpdate(ClientModel model) => await ClientsApi.UpdateClient(model);
    override protected async Task<int> CallDelete(ClientModel model) => await ClientsApi.DeleteClient(model.Id);

    private async void SaveButton_Click(object sender, RoutedEventArgs e) => Save();

    private async void DeleteButton_Click(object sender, RoutedEventArgs e) => Delete();

    private async void FullNameTextBox_TextChanged(object sender, RoutedEventArgs e) => model.IsDirty = model.IdClientNavigation.IsDirty || model.IsDirty;
}