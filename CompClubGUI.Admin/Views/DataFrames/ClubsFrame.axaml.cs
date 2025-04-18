using CompClubGUI.Admin.API.Models;
using System.Collections.Generic;
using System;
using Avalonia.Interactivity;
using System.Threading.Tasks;
using CompClubGUI.Admin.API.APIs;
using CompClubGUI.Admin.Views;

namespace CompClubGUI.Admin;

public partial class ClubsFrame : AdminDataFrame<ClubModel, ClubControl>
{
    public ClubsFrame()
    {
        InitializeComponent();
        dataListGrid = DataListGrid;
    }

    protected async override Task<List<ClubModel>> GetDataFromServer()
    {
        return await ClubsApi.GetClubs();
    }

    protected override void FilterOutData()
    {
        if (ShowClosedFilter.IsChecked == false)
            filteredData.RemoveAll(o => !o.IsAlive);

        string? text = SearchTextBox.Text;
        if (string.IsNullOrWhiteSpace(text)) return;

        filteredData.RemoveAll(o =>
        !o.Name.Contains(text, StringComparison.OrdinalIgnoreCase) &&
        o.Id.ToString() != text &&
        !o.Address.Contains(text, StringComparison.OrdinalIgnoreCase) &&
        !o.Phone.StartsWith(text));
    }

    private void FilterCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateFilters();

    private void SearchTextBox_TextChanged(object sender, RoutedEventArgs e) => UpdateFilters();

    private void AddButton_Click(object sender, RoutedEventArgs e) => AddNewElement();

    protected override ClubModel CreateNewElement()
    {
        return new ClubModel()
        {
            Name = "name",
            Address = "address",
            CreatedAt = DateTime.Now,
            Phone = "phone",
            WorkingHours = "30",
            IsAlive = true,
            IsNewlyCreated = true,
            IsDirty = true
        };
    }

    protected override List<ClubModel> GenerateDebugData()
    {
        List<ClubModel> clubs = [];
        for (int i = 0; i < 10; i++)
        {
            clubs.Add(new ClubModel()
            {
                Id = i,
                Name = $"club{i}",
                Address = "address",
                CreatedAt = DateTime.Now.AddHours(Random.Shared.Next(-200, -1)),
                Phone = "phone",
                IsAlive = Random.Shared.Next(0,10) < 7,
                WorkingHours = Random.Shared.Next(25, 60).ToString(),
            });
        }
        return clubs;
    }
}