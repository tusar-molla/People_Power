using System.ComponentModel.DataAnnotations;

namespace People_Power.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime CheckInTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime? CheckOutTime { get; set; }
        public string? ApprovalStatus { get; set; }
        public ApprovalStatus Status { get; set; }
    }

    public enum ApprovalStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
