using System.Threading.Tasks;
using CompClubGUICore.API.Models;
using CompClubGUICore.API;

namespace CompClubGUI.API.APIs
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class PaymentsApi
    {
        public static async Task<PaymentModel?> GetPaymentInfoAsync()
        {
            ApiResponse response = await ApiClient.CallGet("/api/Payments/get_info");

            string? card = response.GetValue<string>("cardNumber");
            string? cvv = response.GetValue<string>("cvv");

            if (response.StatusCode == 404)
            {
                return null;
            }

            PaymentModel model = new PaymentModel(card, cvv);
            return model;
        }

        public static async Task<int> SetPaymentInfoAsync(string card, string cvv)
        {
            PaymentModel model = new PaymentModel(card, cvv);
            ApiResponse response = await ApiClient.CallPost("/api/Payments/create", model, true);
            return response.StatusCode;
        }

    }
}
