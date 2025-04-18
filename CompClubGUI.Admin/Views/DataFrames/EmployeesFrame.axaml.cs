using CompClubGUI.Admin.API.Models;
using System.Collections.Generic;
using System;
using Avalonia.Interactivity;
using CompClubGUI.Admin.API.APIs;
using System.Threading.Tasks;
using CompClubGUI.Admin.Views;

namespace CompClubGUI.Admin;

public partial class EmployeesFrame : AdminDataFrame<EmployeeModel, EmployeeControl>
{
    public EmployeesFrame()
    {
        InitializeComponent();
        dataListGrid = DataListGrid;
    }

    protected async override Task<List<EmployeeModel>> GetDataFromServer()
    {
        return await EmployeesApi.GetEmployees(AdminApp.CurrentClubID);
    }

    protected override void FilterOutData()
    {

    }


    private void FilterCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateFilters();

    private void AddButton_Click(object sender, RoutedEventArgs e) => AddNewElement();

    protected override EmployeeModel CreateNewElement()
    {
        return new EmployeeModel()
        {
            //Name = "name",
            //IdClub = 1, // TODO: REPLACE THIS HARDCODED ID WITh NORMAL ID GETTING METHODS!!!!
            //Status = "empty",
            //IdTariff = 1,
            IsNewlyCreated = true,
            IsDirty = true
        };
    }

    protected override List<EmployeeModel> GenerateDebugData()
    {
        List<EmployeeModel> spaces = [];
        for (int i = 0; i < 10; i++)
        {
            bool isReserved = Random.Shared.NextSingle() > 0.6;
            spaces.Add(new EmployeeModel
            {
                Id = i,
                //User = isReserved ? $"user{i}" : null,
                //Name = $"place{i}",
                //SessionStartDate = isReserved ? DateTime.Now.AddHours(Random.Shared.Next(-48, 48)) : null,
                //IdClub = 1,
                //Status = "status",
                //IdTariff = 1
            });
        }
        return spaces;
    }
}