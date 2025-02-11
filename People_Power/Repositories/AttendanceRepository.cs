using Microsoft.EntityFrameworkCore;
using People_Power.Data;
using People_Power.Interfaces;
using People_Power.Models;

namespace People_Power.Repositories
{
    public class AttendanceRepository : GenericRepository<Attendance> , IAttendanceRepository
    {
        private readonly AppDbContext _context;
        public AttendanceRepository (AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Get attendance records by EmployeeId
        public async Task<IEnumerable<Attendance>> GetAttendanceByEmployeeIdAsync(int employeeId)
        {
            return await _context.Attendances
                .Where(a => a.EmployeeId == employeeId)
                .Include(a => a.Employee) // Include Employee details
                .ToListAsync();
        }

        // Get attendance records within a specific date range
        public async Task<IEnumerable<Attendance>> GetAttendanceByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Attendances
                .Where(a => a.Date >= startDate && a.Date <= endDate)
                .Include(a => a.Employee)
                .ToListAsync();
        }

        // Get the latest attendance for a specific employee
        public async Task<Attendance?> GetLatestAttendanceForEmployeeAsync(int employeeId)
        {
            return await _context.Attendances
                .Where(a => a.EmployeeId == employeeId)
                .OrderByDescending(a => a.Date)
                .FirstOrDefaultAsync();
        }
    }
}
