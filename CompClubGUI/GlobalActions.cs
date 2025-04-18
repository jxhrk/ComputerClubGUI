using CompClubGUI.API.APIs;

namespace CompClubGUI
{
    internal class GlobalActions
    {
        public static async void UpdateUserInfo()
        {
            await UsersApi.GetAccountInfoAsync();
            AppData.MainModel.User = AppData.CurrentUser;
        }
    }
}
