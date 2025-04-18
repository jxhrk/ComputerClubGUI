using System.Collections.Generic;
using System.Threading.Tasks;
using CompClubGUICore.API;
using CompClubGUI.Admin.API.Models;

namespace CompClubGUI.Admin.API.APIs
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class ClubsApi
    {
        public static async Task<List<ClubModel>> GetClubs()
        {
            ApiResponse response = await ApiClient.CallGet("/api/Club/get_clubs");
            return response.GetValue<List<ClubModel>>("clubs");
        }


        /// <summary>
        /// Create new club
        /// </summary>
        public static async Task<int> CreateClub(ClubModel club)
        {
            object newClub = new
            {
                address = club.Address,
                name = club.Name,
                phone = club.Phone,
                workingHours = club.WorkingHours,
                finances = 0
            };
            ApiResponse response = await ApiClient.CallPost("/api/Club/create_club", newClub, true);
            return response.StatusCode;
        }

        /// <summary>
        /// Delete existing club by it's id
        /// </summary>
        /// <param name="clubId">id of the club to delete</param>
        /// <returns>Status code of the response</returns>
        public static async Task<int> DeleteClub(int clubId)
        {
            ApiResponse response = await ApiClient.CallDelete($"/api/Club/delete_club/{clubId}");
            return response.StatusCode;
        }

        /// <summary>
        /// Update an existing club with new information
        /// </summary>
        /// <param name="place">The ClubModel object containing updated information for the club</param>
        /// <returns>A task representing the asynchronous operation, with an integer status code of the response</returns>
        public static async Task<int> UpdateClub(ClubModel club)
        {
            object updatedClub = new
            {
                address = club.Address,
                name = club.Name,
                phone = club.Phone,
                workingHours = club.WorkingHours
            };
            ApiResponse response = await ApiClient.CallPut($"/api/Club/update_club/{club.Id}", updatedClub);
            return response.StatusCode;
        }
    }
}
