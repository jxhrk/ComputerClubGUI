using Avalonia.Interactivity;
using CompClubGUI.Admin.API.APIs;
using CompClubGUI.Admin.API.Models;
using CompClubGUI.Admin.Views.Controls;
using System.Threading.Tasks;

namespace CompClubGUI.Admin;

public partial class ClubControl : DataFrameListElement<ClubModel>
{
    public ClubControl() : base()
    {
        InitializeComponent();
    }

    override protected async Task<int> CallCreate(ClubModel model) => await ClubsApi.CreateClub(model);
    override protected async Task<int> CallUpdate(ClubModel model) => await ClubsApi.UpdateClub(model);
    override protected async Task<int> CallDelete(ClubModel model) => await ClubsApi.DeleteClub(model.Id);

    private async void SaveButton_Click(object sender, RoutedEventArgs e) => Save();

    private async void DeleteButton_Click(object sender, RoutedEventArgs e) => Delete();

    private async void SelectButton_Click(object sender, RoutedEventArgs e) => AdminApp.CurrentClubID = model.Id;
}