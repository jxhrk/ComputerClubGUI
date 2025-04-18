using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CompClubGUICore.API.Models;
namespace CompClubGUI.Admin.API.Models
{
    /// <summary>
    /// Model for playable gaming place in club (including admin-only info)
    /// </summary>
    public partial class PlaceModel : BaseAdminModel
    {
        [ObservableProperty]
        private string name;
        
        [ObservableProperty]
        private int idClub;

        [ObservableProperty]
        private int idTariff;

        [ObservableProperty]
        private string status;

        public List<BookingModel> Bookings
        {
            set
            {
                CurrentBooking = value.Where(o => o.EndTime > DateTime.Now).FirstOrDefault();
                if (CurrentBooking == null) return;
                //TODO: remove data duplication
                User = CurrentBooking.AccountId.ToString();
                SessionStartDate = CurrentBooking.StartTime;
            }
        }

        // UI-only properties

        [ObservableProperty]
        private BookingModel? currentBooking;

        [ObservableProperty]
        private string? user;

        [ObservableProperty]
        private DateTime? sessionStartDate;

        public bool IsPlaceReserved { get => User != null; set { } }

        public bool IsSessionActive
        {
            get
            {
                if (SessionStartDate == null) return false;
                return DateTime.Now - SessionStartDate >= TimeSpan.Zero;
            }
            set { }
        }
    }
}
