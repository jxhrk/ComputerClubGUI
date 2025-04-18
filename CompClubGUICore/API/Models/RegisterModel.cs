using System.Runtime.Serialization;
namespace CompClubGUICore.API.Models
{
    /// <summary>
    /// RegisterModel
    /// </summary>
    [DataContract]
    public partial class RegisterModel
    {
        public RegisterModel(string login, string password, string firstName, string lastName, string? middleName, string email)
        {
            Login = login;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Email = email;
        }

        [DataMember(Name = "login", EmitDefaultValue = false)]
        public string Login { get; set; }

        [DataMember(Name = "password", EmitDefaultValue = false)]
        public string Password { get; set; }

        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName", EmitDefaultValue = false)]
        public string LastName { get; set; }

        [DataMember(Name = "middleName", EmitDefaultValue = false)]
        public string? MiddleName { get; set; }

        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }
    }
}
