using System.Collections.Generic;
using System.Threading.Tasks;
using CompClubGUICore.API;
using CompClubGUI.Admin.API.Models;

namespace CompClubGUI.Admin.API.APIs
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class EmployeesApi
    {
        public static async Task<List<EmployeeModel>> GetEmployees(int id)
        {
            ApiResponse response = await ApiClient.CallGet($"/api/Employee/get_employees_by_club/{id}");
            return response.GetValue<List<EmployeeModel>>("employees");
        }

        public static async Task<List<EmployeeRoleModel>> GetRoles()
        {
            ApiResponse response = await ApiClient.CallGet("/api/EmployeeRole/get_roles");
            return response.GetValue<List<EmployeeRoleModel>>("roles");
        }


        /// <summary>
        /// Create new club
        /// </summary>
        public static async Task<int> CreateEmployee(EmployeeModel employee)
        {
            object newClub = new
            {
                login = employee.Login,
                password = employee.Password,
                passpordData = employee.PassportData,
                idRole = employee.IdRole,
                idClub = employee.IdClub,
                salary = employee.Salary
            };
            ApiResponse response = await ApiClient.CallPost("/api/Employee/hire_employee", newClub, true);
            return response.StatusCode;
        }

        /// <summary>
        /// Delete existing club by it's id
        /// </summary>
        /// <param name="clubId">id of the club to delete</param>
        /// <returns>Status code of the response</returns>
        public static async Task<int> DeleteEmployee(int id)
        {
            ApiResponse response = await ApiClient.CallDelete($"/api/Employee/fire_employee/{id}");
            return response.StatusCode;
        }

        /// <summary>
        /// Update an existing club with new information
        /// </summary>
        /// <param name="place">The ClubModel object containing updated information for the club</param>
        /// <returns>A task representing the asynchronous operation, with an integer status code of the response</returns>
        public static async Task<int> UpdateEmployee(EmployeeModel employee)
        {
            object updated = new
            {
                login = employee.Login,
                password = employee.Password,
                idRole = employee.IdRole,
                idClub = employee.IdClub,
                salary = employee.Salary
            };
            ApiResponse response = await ApiClient.CallPut($"/api/Employee/update_employee/{employee.Id}", updated);
            return response.StatusCode;
        }
    }
}
