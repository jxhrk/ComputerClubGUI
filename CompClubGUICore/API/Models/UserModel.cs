using CommunityToolkit.Mvvm.ComponentModel;
namespace CompClubGUICore.API.Models
{
    /// <summary>
    /// UserModel
    /// </summary>
    public partial class UserModel : ObservableObject
    {
        public UserModel(string login, string firstName, string lastName, string? middleName, decimal? balance)
        {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Balance = balance;
        }

        [ObservableProperty]
        private string login;

        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private string? middleName;

        [ObservableProperty]
        private decimal? balance;
    }
}
