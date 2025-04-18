using System.Runtime.Serialization;

namespace CompClubGUICore.API.Models;

public class BookingModel
{
    private DateTime startDate;

    [DataMember(Name = "startTime", EmitDefaultValue = false)]
    public DateTime StartTime
    {
        get => startDate;
        set
        {
            startDate = value;
            Date = DateOnly.FromDateTime(value);
            Time = TimeOnly.FromDateTime(value);
            InfoText = DateTime.Now >= value ? "" : "";
        }
    }
    [DataMember(Name = "idWorkingSpace", EmitDefaultValue = false)]
    public int IdWorkingSpace { get; set; }
    [DataMember(Name = "idWorkingSpaceNavigation", EmitDefaultValue = false)]
    public PlaceModel IdWorkingSpaceNavigation { get; set; }
    [DataMember(Name = "endTime", EmitDefaultValue = false)]
    public DateTime EndTime { get; set; }
    [DataMember(Name = "idStatus", EmitDefaultValue = false)]
    public int IdStatus { get; set; }
    [DataMember(Name = "totalCost", EmitDefaultValue = false)]
    public decimal? TotalCost { get; set; }
    [DataMember(Name = "idPaymentMethod", EmitDefaultValue = false)]
    public int? IdPaymentMethod { get; set; }
    [DataMember(Name = "createdAt", EmitDefaultValue = false)]
    public DateTime CreatedAt { get; set; }
    [DataMember(Name = "updatedAt", EmitDefaultValue = false)]
    public DateTime UpdatedAt { get; set; }
    
    [DataMember(Name = "accountId", EmitDefaultValue = false)]
    public int AccountId { get; set; }

    // UI-only properties

    public string InfoText { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
}
