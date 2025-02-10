using Microsoft.EntityFrameworkCore;
using People_Power.Data;
using People_Power.Interfaces;
using People_Power.Models;

namespace People_Power.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context) : base(context)
        {
            context = _context;
        }
        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentIdAsync(int departmentId)
        {
            return await _context.Employees
                .Where(e => e.DepartmentId == departmentId).ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }
    }
}
