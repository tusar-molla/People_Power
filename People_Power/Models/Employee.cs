namespace People_Power.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public string Address { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }

        public ICollection<Leave> Leaves { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Payroll> Payrolls { get; set; }
    }
}
