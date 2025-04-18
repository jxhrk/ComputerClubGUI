using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
namespace CompClubGUI.Admin.API.Models
{
    /// <summary>
    /// Base admin model class with dirty and new flags
    /// </summary>
    public partial class BaseAdminModel : ObservableObject
    {
        [ObservableProperty]
        private int id;

        public BaseAdminModel()
        {
            BindDirtyWithDelay();
        }

        private async void BindDirtyWithDelay()
        {
            await Task.Delay(20);
            PropertyChanged += PlaceModel_PropertyChanged;
        }

        private void PlaceModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsDirty")
                IsDirty = true;
        }

        [ObservableProperty]
        private bool isDirty;

        public bool IsNewlyCreated { get; set; }

        public bool IsExpanded { get; set; }
    }
}
