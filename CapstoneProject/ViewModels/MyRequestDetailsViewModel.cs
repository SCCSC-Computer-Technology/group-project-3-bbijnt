namespace CapstoneProject.ViewModels
{
    public class MyRequestDetailsViewModel
    {
        public int TransactionID { get; set; }
        public DateOnly Date { get; set; }
        public string Status { get; set; } = "";
        public string SpecialRequests { get; set; } = "";
        public int AdditionalPointCost { get; set; }
        public int TotalCost { get; set; }
        public List<MyRequestItemViewModel> Items { get; set; } = new();
    }

    public class MyRequestItemViewModel
    {
        public int ItemID { get; set; }
        public string Description { get; set; } = "";
        public decimal Quantity { get; set; }
        public int PointCost { get; set; }
        public bool IsRG { get; set; }
        public bool IsPAL { get; set; }
    }
}