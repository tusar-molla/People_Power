using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using People_Power.Interfaces;
using People_Power.Models;

namespace People_Power.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }
        public async Task<IActionResult> EmpList()
        {
            var employee = await _employeeRepository.GetAllAsync();
            return View(employee);
        }
        public async Task<IActionResult> EmpDetails(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpGet]
        public async Task<IActionResult> EmpCreate()
        {
            var departments = await _departmentRepository.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmpCreate(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepository.AddAsync(employee);
                return RedirectToAction(nameof(EmpList));
            }
            var departments = await _departmentRepository.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }
        public async Task<IActionResult> EmpEdit(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var departments = await _departmentRepository.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmpEdit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _employeeRepository.Update(employee);
                return RedirectToAction(nameof(EmpList));
            }
            var departments = await _departmentRepository.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }
        public async Task<IActionResult> EmpDelete(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost, ActionName("EmpDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmpDeleteConfirmed(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee != null)
            {
                _employeeRepository.Delete(employee);
            }
            return RedirectToAction(nameof(EmpList));
        }
    }
}

