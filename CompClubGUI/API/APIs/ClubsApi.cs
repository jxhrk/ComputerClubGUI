using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CompClubGUICore.API.Models;
using CompClubGUICore.API;

namespace CompClubGUI.API.APIs
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class ClubsApi
    {
        public static async Task<int> GetClubs()
        {
            ApiResponse response = await ApiClient.CallGet("/api/Club/get_clubs");
            AppData.Clubs = response.GetValue<List<ClubModel>>("clubs");
            return response.StatusCode;
        }

        public static async Task<int> GetPlacesInClub(int clubId)
        {
            ApiResponse response = await ApiClient.CallGet($"/api/WorkingSpace/working_spaces_by_club/{clubId}");
            AppData.CurrentPlaces_TEMP = response.GetValue<List<PlaceModel>>("workingSpaces");
            return response.StatusCode;
        }

        public static async Task<int> SendFeedback(int id, string comment, int mark)
        {
            ApiResponse resp = await ApiClient.CallGet($"/api/WorkingSpace/get_info/{id}");
            int idClub = resp.GetValue<int>("idClub");

            object feedback = new
            {
                clubId = idClub,
                comment = comment,
                rating = mark,
            };

            ApiResponse response = await ApiClient.CallPost("/api/Feedback/create_feedback", feedback, true);
            return response.StatusCode;
        }

        public static async Task<int> GetFeedbacks()
        {
            ApiResponse response = await ApiClient.CallGet("/api/Feedback/get_feedbacks");
            AppData.Feedbacks = response.GetValue<List<FeedbackModel>>("result");
            return response.StatusCode;
        }


        public static async Task<int> GetBookings()
        {
            ApiResponse response = await ApiClient.CallGet("/api/Booking/get_client_bookings");
            AppData.Bookings = response.GetValue<List<BookingModel>>("bookings");
            return response.StatusCode;
        }

        public static async Task<int> CreateBooking(int placeId, DateTime startTime, DateTime endTime)
        {
            object booking = new
            {
                idWorkingSpace = placeId,
                startTime = startTime,
                endTime = endTime,
            };

            ApiResponse response = await ApiClient.CallPost("/api/Booking/create_advanced_booking", booking, true);
            return response.StatusCode;
        }


        public static async Task<int> CancelBooking()
        {
            ApiResponse response = await ApiClient.CallDelete("/api/Booking/advanced_booking_cancellation", true);
            return response.StatusCode;
        }

    }
}
