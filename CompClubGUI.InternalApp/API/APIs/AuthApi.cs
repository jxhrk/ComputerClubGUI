using CompClubGUICore;
using CompClubGUICore.API.Models;
using CompClubGUICore.API;
using System.Threading.Tasks;

namespace CompClubGUI.InternalApp.API.APIs
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class AuthApi
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="body"></param>
        /// <returns>Task of status code</returns>
        public static async Task<int> AuthAsync(AuthModel body)
        {
            ApiResponse response = await ApiClient.CallPost("/api/Account/authentication", body);
            AppInfo.SessionToken = response.GetValue<string>("token");
            return response.StatusCode;
        }

    }
}
