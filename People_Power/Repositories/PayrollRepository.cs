using Microsoft.EntityFrameworkCore;
using People_Power.Data;
using People_Power.Interfaces;
using People_Power.Models;

namespace People_Power.Repositories
{
    public class PayrollRepository : GenericRepository<Payroll>, IPayrollRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Payroll> _dbSet;

        public PayrollRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<Payroll>();
        }
        public async Task<Payroll> GetPayrollByEmployeeIdAsync(int employeeId)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.EmployeeId == employeeId);
        }

        public async Task<IEnumerable<Payroll>> GetPayrollsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet.Where(p => p.PayrollDate >= startDate && p.PayrollDate <= endDate).ToListAsync();
        }
    }
}
