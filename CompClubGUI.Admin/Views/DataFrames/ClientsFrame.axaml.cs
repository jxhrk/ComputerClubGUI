using CompClubGUI.Admin.API.Models;
using System.Collections.Generic;
using Avalonia.Interactivity;
using CompClubGUI.Admin.API.APIs;
using System.Threading.Tasks;
using CompClubGUI.Admin.Views;

namespace CompClubGUI.Admin;

public partial class ClientsFrame : AdminDataFrame<ClientModel, ClientControl>
{
    public ClientsFrame()
    {
        InitializeComponent();
        dataListGrid = DataListGrid;
    }

    protected async override Task<List<ClientModel>> GetDataFromServer()
    {
        return await ClientsApi.GetClients();
    }

    protected override void FilterOutData()
    {

    }


    private void FilterCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateFilters();

    private void AddButton_Click(object sender, RoutedEventArgs e) => AddNewElement();

    protected override ClientModel CreateNewElement()
    {
        return new ClientModel()
        {
            IdClientNavigation = new ClientPersonalModel(),
            IsAlive = true,
            IsNewlyCreated = true,
            IsDirty = true
        };
    }

    protected override List<ClientModel> GenerateDebugData()
    {
        List<ClientModel> spaces = [];
        for (int i = 0; i < 10; i++)
        {
            spaces.Add(new ClientModel
            {
                Id = i,
            });
        }
        return spaces;
    }
}