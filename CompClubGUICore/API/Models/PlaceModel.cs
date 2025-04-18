using System.Runtime.Serialization;
namespace CompClubGUICore.API.Models
{
    /// <summary>
    /// Model for playable gaming place in club (including admin-only info)
    /// </summary>
    [DataContract]
    public partial class PlaceModel
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int ID { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "idClubNavigation", EmitDefaultValue = false)]
        public ClubModel IdClubNavigation { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
