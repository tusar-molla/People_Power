using Microsoft.AspNetCore.Mvc;
using People_Power.Interfaces;
using People_Power.Models;

namespace People_Power.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IGenericRepository<Department> _departmentRepository;
        public DepartmentController(IGenericRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<IActionResult> DeptList()
        {
            var departments = await _departmentRepository.GetAllAsync();
            return View(departments);
        }

        public async Task<IActionResult> DeptCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeptCreate(Department department)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepository.AddAsync(department);
                return RedirectToAction(nameof(DeptList));
            }
            return View(department);
        }
        // Edit department (GET)
        public async Task<IActionResult> DeptEdit(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeptEdit(int id, Department department)
        {

            if (id != department.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _departmentRepository.Update(department);
                return RedirectToAction(nameof(DeptList));
            }
            return View(department);
        }

        public async Task<IActionResult> DeptDelete(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // Delete department (POST)
        [HttpPost, ActionName("DeptDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeptDeleteConfirmed(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department != null)
            {
                _departmentRepository.Delete(department);
            }
            return RedirectToAction(nameof(DeptList));
        }
    }
}
