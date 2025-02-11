namespace People_Power.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Self-referencing property for parent-child relationship
        public int? ParentDepartmentId { get; set; }
        public Department? ParentDepartment { get; set; }
        public ICollection<Department>? SubDepartments { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
