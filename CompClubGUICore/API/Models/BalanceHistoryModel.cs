using System.Runtime.Serialization;

namespace CompClubGUICore.API.Models;

public class BalanceHistoryModel
{
    private decimal _balance;

    [DataMember(Name = "price", EmitDefaultValue = false)]
    public decimal price
    {
        get => _balance;
        set
        {
            _balance = value;
            IsIncome = value > 0;
            InfoText = IsIncome ? "Пополнение" : "Оплата";
        }
    }
    public DateTime createdAt { get; set; }
    public string InfoText { get; set; }
    public bool IsIncome { get; set; }
}
