using Avalonia.Controls;
using Avalonia.Interactivity;
using CompClubGUI.Admin.API.APIs;
using CompClubGUI.Admin.API.Models;
using CompClubGUI.Admin.Views.Controls;
using System.Threading.Tasks;

namespace CompClubGUI.Admin;

public partial class WorkingSpaceControl : DataFrameListElement<PlaceModel>
{
    public WorkingSpaceControl() : base()
    {
        InitializeComponent();
    }

    override protected async Task<int> CallCreate(PlaceModel model) => await PlacesApi.CreateClubPlace(model);
    override protected async Task<int> CallUpdate(PlaceModel model) => await PlacesApi.UpdateClubPlace(model);
    override protected async Task<int> CallDelete(PlaceModel model) => await PlacesApi.DeleteClubPlace(model.Id);

    private async void SaveButton_Click(object sender, RoutedEventArgs e) => Save();

    private async void DeleteButton_Click(object sender, RoutedEventArgs e) => Delete();
}