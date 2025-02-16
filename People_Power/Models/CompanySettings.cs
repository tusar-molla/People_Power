namespace People_Power.Models
{
    public class CompanySettings
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public double StandardWorkingHours { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string TaxNumber { get; set; }
        public DateTime FiscalYearStart { get; set; }
        public DateTime FiscalYearEnd { get; set; }
    }
}
