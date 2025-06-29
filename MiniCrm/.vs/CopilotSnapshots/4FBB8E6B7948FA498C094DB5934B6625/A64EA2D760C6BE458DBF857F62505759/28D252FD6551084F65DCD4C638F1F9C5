namespace MiniCrm.Models
{
    public class SaleModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Title { get; set; }
        private DateTime? _saleDate;
        public DateTime SaleDate
        {
            get => _saleDate ?? DateTime.Now;
            set => _saleDate = value;
        }
        public decimal TotalAmount { get; set; }
    }
}
