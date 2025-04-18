using System;
using CommunityToolkit.Mvvm.ComponentModel;
namespace CompClubGUI.Admin.API.Models
{
    /// <summary>
    /// Model for employee
    /// </summary>
    public partial class EmployeeModel : BaseAdminModel
    {
        [ObservableProperty]
        private string login;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string passportData;

        [ObservableProperty]
        private DateOnly hireDate;

        [ObservableProperty]
        private int idRole;

        [ObservableProperty]
        private int salary;

        [ObservableProperty]
        private int idClub;

        [ObservableProperty]
        private DateTime? lastLogin;

        [ObservableProperty]
        private DateTime? passwordChangedAt;

        [ObservableProperty]
        private DateTime createdAt;

        [ObservableProperty]
        private DateTime? updatedAt;

        // UI-only

        public EmployeeRoleModel Role
        {
            get => AdminApp.EmployeeRoles?.Find(o => o.Id == IdRole) ?? new();
            set => IdRole = value.Id;
        }
    }
}
