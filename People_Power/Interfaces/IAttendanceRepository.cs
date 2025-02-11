using People_Power.Models;

namespace People_Power.Interfaces
{
    public interface IAttendanceRepository :IGenericRepository<Attendance>
    {
        Task<IEnumerable<Attendance>> GetAttendanceByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<Attendance>> GetAttendanceByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<Attendance?> GetLatestAttendanceForEmployeeAsync(int employeeId);
    }
}
