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
    public partial class TariffsApi
    {
        /// <summary>
        /// Get all Tariffs from club by club's id
        /// </summary>
        public static async Task<List<TariffModel>> GetTariffs()
        {
            ApiResponse response = await ApiClient.CallGet($"/api/Tariff/get_tariffs");
            return response.GetValue<List<TariffModel>>("tariffs");
        }

        /// <summary>
        /// Create new Tariff in club
        /// </summary>
        public static async Task<int> CreateTariff(TariffModel Tariff)
        {
            object tariff = new
            {
                name = Tariff.Name,
                pricePerMinute = Tariff.PricePerMinute,
            };
            ApiResponse response = await ApiClient.CallPost("/api/Tariff/create_tariff", tariff, true);
            return response.StatusCode;
        }
        
        /// <summary>
        /// Delete existing club Tariff by it's id
        /// </summary>
        /// <param name="spaceId">id of the Tariff to delete</param>
        /// <returns>Status code of the response</returns>
        public static async Task<int> DeleteTariff(int spaceId)
        {
            ApiResponse response = await ApiClient.CallDelete($"/api/Tariff/delete_tariff/{spaceId}");            
            return response.StatusCode;
        }
        
        /// <summary>
        /// Update an existing club Tariff with new information
        /// </summary>
        /// <param name="Tariff">The TariffModel object containing updated information for the club Tariff</param>
        /// <returns>A task representing the asynchronous operation, with an integer status code of the response</returns>
        public static async Task<int> UpdateTariff(TariffModel Tariff)
            
        {
            object tariff = new
            {
                name = Tariff.Name,
                pricePerMinute = Tariff.PricePerMinute,
            };
            ApiResponse response = await ApiClient.CallPut($"/api/Tariff/update_tariff/{Tariff.Id}", tariff);
            return response.StatusCode;
        }
    }
}
