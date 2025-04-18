using Avalonia.Controls;
using CompClubGUI.Admin.API.APIs;
using CompClubGUI.Admin.Views;
using CompClubGUICore.Views;

namespace CompClubGUI.Admin;

public partial class AdminView : BaseMainAppView
{
    public AdminDataFrame? DataFrame;

    public AdminView()
    {
        InitializeComponent();
        GetEmployeesRoles();
    }

    private async void GetEmployeesRoles()
    {
        AdminApp.EmployeeRoles = await EmployeesApi.GetRoles();
    }

    private void ListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataFrame != null && DataFrame.HasUnsavedChanged())
        {
            ConfirmationWindow confirmation = new(null, SwitchToFrame, null);
            confirmation.ShowDialog(MainWindow.Instance);
            return;
        }

        SwitchToFrame();
    }

    public void SwitchToFrame()
    {
        int index = MenuList.SelectedIndex;
        FrameContent.Children.Clear();
        DataFrame = index switch
        {
            0 => new ClubsFrame(),
            1 => new WorkingSpacesFrame(),
            2 => new ClientsFrame(),
            3 => new EmployeesFrame(),
            4 => new TariffsFrame(),
            5 => new EquipmentsFrame(),
            _ => null,
        };
        if (DataFrame == null) { return; }
        FrameContent.Children.Add(DataFrame);
    }
}