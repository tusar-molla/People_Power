namespace People_Power.Models
{
    public class Leave
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public string? LeaveType { get; set; } // e.g. Paid, Unpaid, Sick, etc.
        public string? Status { get; set; } // e.g. Pending, Approved, Rejected
        public string? Reason { get; set; }
        public int? ApprovedById { get; set; }
        public User? ApprovedBy { get; set; }
        public DateTime DateRequested { get; set; }
    }
}
