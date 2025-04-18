using CommunityToolkit.Mvvm.ComponentModel;
namespace CompClubGUICore.API.Models
{
    /// <summary>
    /// PaymentModel
    /// </summary>
    public partial class PaymentModel : ObservableObject
    {
        public PaymentModel(string cardNumber, string cvv)
        {
            this.cardNumber = cardNumber;
            this.cvv = cvv;
        }

        [ObservableProperty]
        private string? cardNumber;

        [ObservableProperty]
        private string? cvv;
    }
}
