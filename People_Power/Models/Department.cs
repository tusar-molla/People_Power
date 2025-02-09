﻿namespace People_Power.Models
{
    public class Department
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}
        public ICollection<Employee>? Employees {get; set;}
    }
}
