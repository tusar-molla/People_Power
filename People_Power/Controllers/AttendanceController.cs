using Microsoft.AspNetCore.Mvc;
using People_Power.Interfaces;
using People_Power.Models;

namespace People_Power.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AttendanceController(IAttendanceRepository attendanceRepository, IEmployeeRepository employeeRepository)
        {
            _attendanceRepository = attendanceRepository;
            _employeeRepository = employeeRepository;
        }
       
        public async Task<IActionResult> AttdnceList()
        {
            var attendances = await _attendanceRepository.GetAllAsync();
            ViewBag.Employees = await _employeeRepository.GetAllAsync();
            return View(attendances);
        }

        public async Task<IActionResult> AttdnceCreate()
        {
            ViewBag.Employees = await _employeeRepository.GetAllAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttdnceCreate(Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                await _attendanceRepository.AddAsync(attendance);
                return RedirectToAction(nameof(AttdnceList));
            }

            ViewBag.Employees = await _employeeRepository.GetAllAsync();
            return View(attendance);
        }
        public async Task<IActionResult> AttdnceEdit(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            ViewBag.Employees = await _employeeRepository.GetAllAsync();
            return View(attendance);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttdnceEdit(int id, Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                 _attendanceRepository.Update(attendance);
                return RedirectToAction(nameof(AttdnceList));
            }

            ViewBag.Employees = await _employeeRepository.GetAllAsync();
            return View(attendance);
        }
        public async Task<IActionResult> AttdnceDelete(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }
        [HttpPost, ActionName("AttdnceDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttdnceDeleteConfirmed(int id)
        {
            var attandance = await _attendanceRepository.GetByIdAsync(id);
             _attendanceRepository.Delete(attandance);
            return RedirectToAction(nameof(AttdnceList));
        }
    }
}
