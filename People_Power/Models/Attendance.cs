﻿namespace People_Power.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public DateTime Date { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public string ApprovalStatus { get; set; }
    }
}
