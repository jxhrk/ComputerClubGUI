using CompClubGUICore;
using CompClubGUICore.API.Models;
using CompClubGUICore.API;
using System.Threading.Tasks;

namespace CompClubGUI.API.APIs
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class AuthApi
    {
        /// <summary>
        /// Authorize into user account
        /// </summary>
        /// <returns>Task of status code</returns>
        public static async Task<int> AuthAsync(AuthModel body)
        {
            ApiResponse response = await ApiClient.CallPost("/api/Account/authentication", body);
            AppInfo.SessionToken = response.GetValue<string>("token");
            return response.StatusCode;
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <returns>Task of status code</returns>
        public static async Task<int> RegisterAsync(RegisterModel body)
        {
            ApiResponse response = await ApiClient.CallPost("/api/Client/create_client", body);
            return response.StatusCode;
        }

    }
}
