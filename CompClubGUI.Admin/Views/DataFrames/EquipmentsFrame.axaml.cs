using CompClubGUI.Admin.API.Models;
using System.Collections.Generic;
using System;
using Avalonia.Interactivity;
using CompClubGUI.Admin.API.APIs;
using System.Threading.Tasks;
using CompClubGUI.Admin.Views;

namespace CompClubGUI.Admin;

public partial class EquipmentsFrame : AdminDataFrame<EquipmentModel, EquipmentControl>
{
    public EquipmentsFrame()
    {
        InitializeComponent();
        dataListGrid = DataListGrid;
    }

    protected async override Task<List<EquipmentModel>> GetDataFromServer()
    {
        return await EquipmentApi.GetEquipments(AdminApp.CurrentClubID);
    }

    protected override void FilterOutData()
    {

    }


    private void FilterCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateFilters();

    private void AddButton_Click(object sender, RoutedEventArgs e) => AddNewElement();

    protected override EquipmentModel CreateNewElement()
    {
        return new EquipmentModel()
        {
            IsNewlyCreated = true,
            IsDirty = true
        };
    }

    protected override List<EquipmentModel> GenerateDebugData()
    {
        List<EquipmentModel> equipments = [];
        for (int i = 0; i < 10; i++)
        {
            bool isReserved = Random.Shared.NextSingle() > 0.6;
            equipments.Add(new EquipmentModel
            {
                Id = i,
            });
        }
        return equipments;
    }
}