using People_Power.Models;

namespace People_Power.Interfaces
{
    public interface ILeaveRepository : IGenericRepository<Leave>
    {
        Task<IEnumerable<Leave>> GetLeavesByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<Leave>> GetApprovedLeavesAsync();
        Task<IEnumerable<Leave>> GetLeavesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Leave>> GetLeavesByStatusAsync(string status);
        IEnumerable<Employee> GetEmployees();
        IEnumerable<User> GetUsers();

    }
}
