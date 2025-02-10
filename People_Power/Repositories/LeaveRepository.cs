using People_Power.Data;
using People_Power.Interfaces;
using People_Power.Models;
using Microsoft.EntityFrameworkCore;

namespace People_Power.Repositories
{
    public class LeaveRepository : GenericRepository<Leave>, ILeaveRepository
    {
        private readonly AppDbContext _context;

        public LeaveRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public IEnumerable<Employee> GetEmployees()
        {
            var employees = _context.Employees.ToList();
            employees.Insert(0, new Employee { Id = 0, Name = "Select Employee" });  // Adding a null option
            return employees;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _context.Users.ToList();
            users.Insert(0, new User { Id = 0, UserName = "Select User" });  // Adding a null option
            return users;
        }

        // Get leaves by EmployeeId
        public async Task<IEnumerable<Leave>> GetLeavesByEmployeeIdAsync(int employeeId)
        {
            return await _context.Leaves
                .Where(l => l.EmployeeId == employeeId)
                .Include(l => l.Employee) 
                .Include(l => l.ApprovedBy)
                .ToListAsync();
        }

        // Get all approved leaves
        public async Task<IEnumerable<Leave>> GetApprovedLeavesAsync()
        {
            return await _context.Leaves
                .Where(l => l.Status == "Approved")
                .Include(l => l.Employee)
                .Include(l => l.ApprovedBy)
                .ToListAsync();
        }

        // Get leaves by date range
        public async Task<IEnumerable<Leave>> GetLeavesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Leaves
                .Where(l => l.LeaveStartDate >= startDate && l.LeaveEndDate <= endDate)
                .Include(l => l.Employee) 
                .Include(l => l.ApprovedBy)
                .ToListAsync();
        }

        // Get leaves by status
        public async Task<IEnumerable<Leave>> GetLeavesByStatusAsync(string status)
        {
            return await _context.Leaves
                .Where(l => l.Status == status)
                .Include(l => l.Employee)
                .Include(l => l.ApprovedBy)
                .ToListAsync();
        }
    }
}
