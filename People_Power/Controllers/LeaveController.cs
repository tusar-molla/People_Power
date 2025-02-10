using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using People_Power.Data;
using People_Power.Interfaces;
using People_Power.Models;

namespace People_Power.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly AppDbContext _context;
        public LeaveController(ILeaveRepository leaveRepository, AppDbContext context)
        {
            _leaveRepository = leaveRepository;
            _context = context;
        }
        public async Task<IActionResult> LeaveList()
        {
            var leaves = await _leaveRepository.GetAllAsync();
            var employees = _leaveRepository.GetEmployees();
            var users = _leaveRepository.GetUsers();
            return View(leaves);
        }
        public async Task<IActionResult> LeaveListByID(int id)
        {
            var leave = await _leaveRepository.GetByIdAsync(id);
            if (leave == null)
            {
                return NotFound();
            }
            return View(leave);
        }
        [HttpGet]
        public async Task <IActionResult> CreateLeave()
        {
            var employees = _leaveRepository.GetEmployees();
            var users = _leaveRepository.GetUsers();

            ViewBag.Employees = new SelectList(employees, "Id", "Name");
            ViewBag.Users = new SelectList(users, "Id", "UserName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLeave(Leave leave)
        {            
            if (ModelState.IsValid)
            {
                await _leaveRepository.AddAsync(leave);
                return RedirectToAction(nameof(LeaveList));
            }
            return View(leave);
        }
        public async Task<IActionResult> EditLeave(int id)
        {
            var leave = await _leaveRepository.GetByIdAsync(id);
            if (leave == null)
            {
                return NotFound();
            }
            var employees = _leaveRepository.GetEmployees();
            var users = _leaveRepository.GetUsers();

            ViewBag.Employees = new SelectList(employees, "Id", "Name");
            ViewBag.Users = new SelectList(users, "Id", "UserName");
            return View(leave);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLeave(int id, Leave leave)
        {
            if (id != leave.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     _leaveRepository.Update(leave);
                }
                catch (DbUpdateConcurrencyException)
                {
                    NotFound();
                }
                var employees = _leaveRepository.GetEmployees();
                var users = _leaveRepository.GetUsers();

                ViewBag.Employees = new SelectList(employees, "Id", "Name");
                ViewBag.Users = new SelectList(users, "Id", "UserName");
                return RedirectToAction(nameof(LeaveList));
            }
            return View(leave);
        }        
        public async Task<IActionResult> DeleteLeave(int id)
        {
            var leave = await _leaveRepository.GetByIdAsync(id);
            var users = _leaveRepository.GetUsers();
            var employees = _leaveRepository.GetEmployees();
            if (leave == null)
            {
                return NotFound();
            }
            return View(leave);
        }

        [HttpPost, ActionName("DeleteLeave")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLeaveConfirmed(int id)
        {
            var leave = await _leaveRepository.GetByIdAsync(id);
            _leaveRepository.Delete(leave);
            return RedirectToAction(nameof(LeaveList));
        }
    }
}
