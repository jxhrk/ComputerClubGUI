using System.Collections.Generic;
using System.Threading.Tasks;
using CompClubGUICore.API.Models;
using CompClubGUICore.API;

namespace CompClubGUI.API.APIs
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class UsersApi
    {
        public static async Task<int> GetAccountInfoAsync()
        {
            ApiResponse response = await ApiClient.CallGet("/api/Account/get_info");
            AppData.CurrentUser = response.GetValue<UserModel>("idClientNavigation");
            if (AppData.CurrentUser != null)
            {
                AppData.CurrentUser.Balance = response.GetValue<decimal>("balance");
            }

            return response.StatusCode;
        }
        public static async Task<int> PutAccountBalance(decimal money)
        {
            var data = new
            {
                money = money
            };
            ApiResponse response = await ApiClient.CallPost("/api/Account/add_balance", data, true);
            return response.StatusCode;
        }

        public static async Task<int> GetBalanceHistory()
        {
            ApiResponse response = await ApiClient.CallGet("/api/Account/balance_history");
            AppData.BalanceHistory = response.GetValue<List<BalanceHistoryModel>>("history");
            return response.StatusCode;
        }

        public static async Task<int> ResetPasswordAsync(string email)
        {
            ApiResponse response = await ApiClient.CallPost($"/api/Account/change_password_by_email/{email}", null, false);
            return response.StatusCode;
        }

    }
}
