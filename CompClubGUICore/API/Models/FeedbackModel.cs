using System.Runtime.Serialization;

namespace CompClubGUICore.API.Models;

public class FeedbackModel
{
    public FeedbackModel()
    {

    }

    [DataMember(Name = "address", EmitDefaultValue = false)]
    public string Address { get; set; }

    [DataMember(Name = "comment", EmitDefaultValue = false)]
    public string Comment { get; set; }

    [DataMember(Name = "rating", EmitDefaultValue = false)]
    public int Rating { get; set; }

    [DataMember(Name = "createdAt", EmitDefaultValue = false)]
    public DateTime CreatedAt { get; set; }

    [DataMember(Name = "name", EmitDefaultValue = false)]
    public string Name { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Rating}): {Comment}";
    }
}
