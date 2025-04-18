using System.Runtime.Serialization;
namespace CompClubGUICore.API.Models
{
    /// <summary>
    /// Model for club
    /// </summary>
    [DataContract]
    public partial class ClubModel
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int ID { get; set; }
        
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string Address { get; set; }
        
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }
        
        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public string Phone { get; set; }
                
        [DataMember(Name = "workingHours", EmitDefaultValue = false)]
        public string WorkingHours { get; set; } 
        
        [DataMember(Name = "createdAt", EmitDefaultValue = false)]
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return Address;
        }
    }
}
