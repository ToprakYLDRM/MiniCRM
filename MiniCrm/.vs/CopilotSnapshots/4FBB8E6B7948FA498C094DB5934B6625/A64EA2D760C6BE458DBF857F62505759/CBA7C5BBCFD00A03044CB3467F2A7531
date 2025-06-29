namespace MiniCrm.Models
{
    public class OfferModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        private DateTime? _offerDate;
        public DateTime OfferDate
        {
            get => _offerDate ?? DateTime.Now;
            set => _offerDate = value;
        }
        public string Status { get; set; }
    }
}
