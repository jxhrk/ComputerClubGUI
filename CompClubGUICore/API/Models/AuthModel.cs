using System.Runtime.Serialization;
namespace CompClubGUICore.API.Models
{
    /// <summary>
    /// AuthModel
    /// </summary>
    [DataContract]
    public partial class AuthModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthModel" /> class.
        /// </summary>
        /// <param name="login">login.</param>
        /// <param name="password">password.</param>
        public AuthModel(string login, string password)
        {
            Login = login;
            Password = password;
        }

        /// <summary>
        /// Gets or Sets Login
        /// </summary>
        [DataMember(Name = "login", EmitDefaultValue = false)]
        public string Login { get; set; }

        /// <summary>
        /// Gets or Sets Password
        /// </summary>
        [DataMember(Name = "password", EmitDefaultValue = false)]
        public string Password { get; set; }
    }
}
