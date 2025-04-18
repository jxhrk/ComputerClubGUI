using Avalonia.Controls;
using Avalonia.Interactivity;
using CompClubGUI.Admin.API.APIs;
using CompClubGUI.Admin.API.Models;
using CompClubGUI.Admin.Views.Controls;
using System.Threading.Tasks;

namespace CompClubGUI.Admin;

public partial class TariffControl : DataFrameListElement<TariffModel>
{
    public TariffControl() : base()
    {
        InitializeComponent();
    }

    override protected async Task<int> CallCreate(TariffModel model) => await TariffsApi.CreateTariff(model);
    override protected async Task<int> CallUpdate(TariffModel model) => await TariffsApi.UpdateTariff(model);
    override protected async Task<int> CallDelete(TariffModel model) => await TariffsApi.DeleteTariff(model.Id);

    private async void SaveButton_Click(object sender, RoutedEventArgs e) => Save();

    private async void DeleteButton_Click(object sender, RoutedEventArgs e) => Delete();
}