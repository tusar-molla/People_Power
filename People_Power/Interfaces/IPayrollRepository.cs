using People_Power.Models;

namespace People_Power.Interfaces
{
    public interface IPayrollRepository : IGenericRepository<Payroll>
    {
        Task<Payroll> GetPayrollByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<Payroll>> GetPayrollsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
