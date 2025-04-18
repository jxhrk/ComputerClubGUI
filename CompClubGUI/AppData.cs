using System.Collections.Generic;
using CompClubGUI.ViewModels;
using CompClubGUICore.API.Models;

namespace CompClubGUI
{
    public static class AppData
    {
        public static UserModel? CurrentUser;

        public static List<BalanceHistoryModel>? BalanceHistory;

        public static List<ClubModel>? Clubs;
        public static List<PlaceModel>? CurrentPlaces_TEMP;

        public static List<BookingModel>? Bookings;

        public static List<FeedbackModel>? Feedbacks;

        public static MainViewModel MainModel = new();
    }
}
