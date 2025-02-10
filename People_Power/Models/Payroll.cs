namespace People_Power.Models
{
    public class Payroll
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
        public decimal TotalSalary => BasicSalary + Bonus - Deduction; // Calculated Total Salary
        public DateTime PayrollDate { get; set; }
    }
}
