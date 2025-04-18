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
    public partial class EquipmentApi
    {
        /// <summary>
        /// Get all Equipments from club by club's id
        /// </summary>
        public static async Task<List<EquipmentModel>> GetEquipments(int clubId)
        {
            ApiResponse response = await ApiClient.CallGet($"/api/Equipment/get_equipment_by_club/{clubId}");
            return response.GetValue<List<EquipmentModel>>("equipments");
        }

        /// <summary>
        /// Create new Equipment in club
        /// </summary>
        public static async Task<int> CreateEquipment(EquipmentModel Equipment)
        {
            object equipment = new
            {
                type = Equipment.Type,
                name = Equipment.Name,
                specification = Equipment.Specification,
                purchasePrice = Equipment.PurchasePrice,
                idClub = Equipment.IdClub,
            };
            ApiResponse response = await ApiClient.CallPost("/api/Equipment/create_equipment", equipment, true);
            return response.StatusCode;
        }
        
        /// <summary>
        /// Delete existing club Equipment by it's id
        /// </summary>
        /// <param name="id">id of the Equipment to delete</param>
        /// <returns>Status code of the response</returns>
        public static async Task<int> DeleteEquipment(int id)
        {
            ApiResponse response = await ApiClient.CallDelete($"/api/Equipment/delete/{id}");            
            return response.StatusCode;
        }
        
        /// <summary>
        /// Update an existing club Equipment with new information
        /// </summary>
        /// <param name="Equipment">The EquipmentModel object containing updated information for the club Equipment</param>
        /// <returns>A task representing the asynchronous operation, with an integer status code of the response</returns>
        public static async Task<int> UpdateEquipment(EquipmentModel Equipment)
            
        {
            object equipment = new
            {
                type = Equipment.Type,
                name = Equipment.Name,
                specification = Equipment.Specification,
                purchasePrice = Equipment.PurchasePrice,
                idClub = Equipment.IdClub,
            };
            ApiResponse response = await ApiClient.CallPut($"/api/Equipment/update/{Equipment.Id}", equipment);
            return response.StatusCode;
        }
    }
}
