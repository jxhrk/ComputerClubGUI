using CompClubGUICore;
using CompClubGUICore.API;
using System.Threading.Tasks;
using System.Collections.Generic;
using CompClubGUI.Admin.API.Models;

namespace CompClubGUI.Admin.API.APIs
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class PlacesApi
    {
        /// <summary>
        /// Get all places from club by club's id
        /// </summary>
        public static async Task<List<PlaceModel>> GetClubPlaces(int ClubID)
        {
            ApiResponse response = await ApiClient.CallGet($"/api/WorkingSpace/working_spaces_by_club/{ClubID}");
            return response.GetValue<List<PlaceModel>>("workingSpaces");
        }

        /// <summary>
        /// Create new place in club
        /// </summary>
        public static async Task<int> CreateClubPlace(PlaceModel place)
        {
            object clubPlace = new
            {
                idClub = place.IdClub,
                name = place.Name,
                status = place.Status,
                idTariff = place.IdTariff
            };
            ApiResponse response = await ApiClient.CallPost("/api/WorkingSpace/create_working_space", clubPlace, true);
            return response.StatusCode;
        }
        
        /// <summary>
        /// Delete existing club place by it's id
        /// </summary>
        /// <param name="spaceId">id of the place to delete</param>
        /// <returns>Status code of the response</returns>
        public static async Task<int> DeleteClubPlace(int spaceId)
        {
            ApiResponse response = await ApiClient.CallDelete($"/api/WorkingSpace/delete/{spaceId}");            
            return response.StatusCode;
        }
        
        /// <summary>
        /// Update an existing club place with new information
        /// </summary>
        /// <param name="place">The PlaceModel object containing updated information for the club place</param>
        /// <returns>A task representing the asynchronous operation, with an integer status code of the response</returns>
        public static async Task<int> UpdateClubPlace(PlaceModel place)
            
        {
            object clubPlace = new
            {
                idClub = place.IdClub,
                name = place.Name,
                status = place.Status,
                idTariff = place.IdTariff
            };
            ApiResponse response = await ApiClient.CallPut($"/api/WorkingSpace/update/{place.Id}", clubPlace);
            return response.StatusCode;
        }

        /// <summary>
        /// Get the information of a place by it's id
        /// </summary>
        /// <param name="idSpace">The id of the place to get the information of</param>
        /// <returns>A task representing the asynchronous operation, with the status code of the response</returns>
        public static async Task<int> GetSpaceById(int idSpace)
        {
            ApiResponse response = await ApiClient.CallGet($"/api/WorkingSpace/get_info/{idSpace}");
            return response.StatusCode;
        }
    }
}
