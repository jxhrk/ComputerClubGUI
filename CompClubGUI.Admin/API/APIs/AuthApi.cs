using CompClubGUICore;
using CompClubGUICore.API.Models;
using CompClubGUICore.API;
using System.Threading.Tasks;

namespace CompClubGUI.Admin.API.APIs
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class AdminAuthApi
    {
        /// <summary>
        /// Authorize into admin account
        /// </summary>
        /// <returns>Task of status code</returns>
        public static async Task<int> AuthAsync(AuthModel body)
        {
            ApiResponse response = await ApiClient.CallPost("/api/Employee/authorization", body);
            AppInfo.SessionToken = response.GetValue<string>("token");
            return response.StatusCode;
        }

    }
}
