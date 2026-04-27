namespace CapstoneProject.ViewModels
{
    public class MyRequestViewModel
    {
        public int TransactionID { get; set; }
        public DateOnly Date { get; set; }
        public string Status { get; set; } = "";
        public int TotalCost { get; set; }
        public string SpecialRequests { get; set; } = "";
    }
}