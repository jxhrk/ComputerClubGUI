using CompClubGUI.Admin.API.Models;
using System.Collections.Generic;
using System;
using Avalonia.Interactivity;
using CompClubGUI.Admin.API.APIs;
using System.Threading.Tasks;
using CompClubGUI.Admin.Views;

namespace CompClubGUI.Admin;

public partial class WorkingSpacesFrame : AdminDataFrame<PlaceModel, WorkingSpaceControl>
{
    public WorkingSpacesFrame()
    {
        InitializeComponent();
        dataListGrid = DataListGrid;
    }

    protected async override Task<List<PlaceModel>> GetDataFromServer()
    {
        return await PlacesApi.GetClubPlaces(AdminApp.CurrentClubID);
    }

    protected override void FilterOutData()
    {
        if (ReservedFilter.IsChecked == true)
            filteredData.RemoveAll(o => !o.IsPlaceReserved);

        if (ActiveFilter.IsChecked == true)
            filteredData.RemoveAll(o => !o.IsSessionActive);
    }


    private void FilterCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateFilters();

    private void AddButton_Click(object sender, RoutedEventArgs e) => AddNewElement();

    protected override PlaceModel CreateNewElement()
    {
        return new PlaceModel()
        {
            Name = "name",
            IdClub = AdminApp.CurrentClubID,
            Status = "empty",
            IdTariff = 1,
            IsNewlyCreated = true,
            IsDirty = true
        };
    }

    protected override List<PlaceModel> GenerateDebugData()
    {
        List<PlaceModel> spaces = [];
        for (int i = 0; i < 10; i++)
        {
            bool isReserved = Random.Shared.NextSingle() > 0.6;
            spaces.Add(new PlaceModel
            {
                Id = i,
                User = isReserved ? $"user{i}" : null,
                Name = $"place{i}",
                SessionStartDate = isReserved ? DateTime.Now.AddHours(Random.Shared.Next(-48, 48)) : null,
                IdClub = 1,
                Status = "status",
                IdTariff = 1
            });
        }
        return spaces;
    }
}