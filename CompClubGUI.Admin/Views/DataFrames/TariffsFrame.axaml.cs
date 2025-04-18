using CompClubGUI.Admin.API.Models;
using System.Collections.Generic;
using System;
using Avalonia.Interactivity;
using CompClubGUI.Admin.API.APIs;
using System.Threading.Tasks;
using CompClubGUI.Admin.Views;
using Avalonia.Controls;

namespace CompClubGUI.Admin;

public partial class TariffsFrame : AdminDataFrame<TariffModel, TariffControl>
{
    public TariffsFrame()
    {
        InitializeComponent();
        dataListGrid = DataListGrid;
    }

    protected async override Task<List<TariffModel>> GetDataFromServer()
    {
        return await TariffsApi.GetTariffs();
    }

    protected override void FilterOutData()
    {
        if (!string.IsNullOrWhiteSpace(FilterTextBox.Text))
        {
            filteredData.RemoveAll(o =>
                !o.Name.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase) &&
                o.PricePerMinute.ToString() != FilterTextBox.Text
            );
        }
    }


    private void FilterCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateFilters();
    private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e) => UpdateFilters();

    private void AddButton_Click(object sender, RoutedEventArgs e) => AddNewElement();

    protected override TariffModel CreateNewElement()
    {
        return new TariffModel()
        {
            Name = "name",
            PricePerMinute = 10,
            IsNewlyCreated = true,
            IsDirty = true
        };
    }

    protected override List<TariffModel> GenerateDebugData()
    {
        List<TariffModel> tariffs = [];
        for (int i = 0; i < 10; i++)
        {
            tariffs.Add(new TariffModel
            {
                Id = i,
                Name = $"tariff{i}",
                PricePerMinute = Random.Shared.Next(5, 100),
            });
        }
        return tariffs;
    }
}