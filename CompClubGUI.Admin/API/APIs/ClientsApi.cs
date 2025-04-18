using System.Collections.Generic;
using System.Threading.Tasks;
using CompClubGUICore.API;
using CompClubGUI.Admin.API.Models;

namespace CompClubGUI.Admin.API.APIs
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class ClientsApi
    {
        public static async Task<List<ClientModel>> GetClients()
        {
            ApiResponse response = await ApiClient.CallGet("/api/Account/get_accounts");
            return response.GetValue<List<ClientModel>>("accounts");
        }


        /// <summary>
        /// Create new club
        /// </summary>
        public static async Task<int> CreateClient(ClientModel client)
        {
            object newClub = new
            {
                firstName = client.IdClientNavigation.FirstName,
                middleName = client.IdClientNavigation.MiddleName,
                lastName = client.IdClientNavigation.LastName,
                login = client.Login,
                password = client.Password,
                email = client.Email,
            };
            ApiResponse response = await ApiClient.CallPost("/api/Client/create_client", newClub, true);
            return response.StatusCode;
        }

        /// <summary>
        /// Delete existing club by it's id
        /// </summary>
        /// <param name="clubId">id of the club to delete</param>
        /// <returns>Status code of the response</returns>
        public static async Task<int> DeleteClient(int id)
        {
            ApiResponse response = await ApiClient.CallPut($"/api/Account/deactivate_account/{id}", null);
            return response.StatusCode;
        }

        /// <summary>
        /// Update an existing club with new information
        /// </summary>
        /// <param name="place">The ClubModel object containing updated information for the club</param>
        /// <returns>A task representing the asynchronous operation, with an integer status code of the response</returns>
        public static async Task<int> UpdateClient(ClientModel client)
        {
            object updated = new
            {
                login = client.Login,
                password = client.Password,
                email = client.Email,
            };
            ApiResponse response = await ApiClient.CallPut($"/api/Account/update/{client.Id}", updated);

            object updatedFullName = new
            {
                firstName = client.IdClientNavigation.FirstName,
                middleName = client.IdClientNavigation.MiddleName,
                lastName = client.IdClientNavigation.LastName,
            };
            ApiResponse response1 = await ApiClient.CallPut($"/api/Client/update_client/{client.Id}", updatedFullName);

            if (response.StatusCode != 204)
                return response.StatusCode;

            return response1.StatusCode;
        }
    }
}
