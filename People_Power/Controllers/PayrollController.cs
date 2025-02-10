using Microsoft.AspNetCore.Mvc;
using People_Power.Interfaces;
using People_Power.Models;

namespace People_Power.Controllers
{
    public class PayrollController : Controller
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public PayrollController(IPayrollRepository payrollRepository, IEmployeeRepository employeeRepository)
        {
            _payrollRepository = payrollRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task<IActionResult> PrList()
        {
            var employees = await _employeeRepository.GetAllAsync();
            ViewBag.Employees = employees;
            var payrolls = await _payrollRepository.GetAllAsync();
            return View(payrolls);
        }
        public async Task<IActionResult> PrDetails(int id)
        {
            var employees = await _employeeRepository.GetAllAsync();
            var payroll = await _payrollRepository.GetByIdAsync(id);
            if (payroll == null)
            {
                return NotFound();
            }
            return View(payroll);
        }

        public async Task<IActionResult> PrCreate()
        {
            // Populate the Employee list for the dropdown (for foreign key reference)
            var employees = await _employeeRepository.GetAllAsync();
            ViewBag.Employees = employees;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PrCreate(Payroll payroll)
        {
            if (ModelState.IsValid)
            {
                await _payrollRepository.AddAsync(payroll);
                return RedirectToAction(nameof(PrList));
            }

            // If validation fails, reload employee list
            var employees = await _employeeRepository.GetAllAsync();
            ViewBag.Employees = employees;
            return View(payroll);
        }
        public async Task<IActionResult> PrEdit(int id)
        {
            var payroll = await _payrollRepository.GetByIdAsync(id);
            if (payroll == null)
            {
                return NotFound();
            }

            // Populate the Employee list for the dropdown (for foreign key reference)
            var employees = await _employeeRepository.GetAllAsync();
            ViewBag.Employees = employees;

            return View(payroll);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PrEdit(int id, Payroll payroll)
        {
            if (id != payroll.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _payrollRepository.Update(payroll);
                }
                catch
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(PrList));
            }

            // If validation fails, reload employee list
            var employees = await _employeeRepository.GetAllAsync();
            ViewBag.Employees = employees;
            return View(payroll);
        }
        public async Task<IActionResult> PrDelete(int id)
        {
            var payroll = await _payrollRepository.GetByIdAsync(id);
            if (payroll == null)
            {
                return NotFound();
            }
            return View(payroll);
        }

        // POST: Payroll/Delete/5
        [HttpPost, ActionName("PrDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PrDeleteConfirmed(int id)
        {
            var payroll = await _payrollRepository.GetByIdAsync(id);
            if (payroll != null)
            {
                _payrollRepository.Delete(payroll);
            }
            return RedirectToAction(nameof(PrList));
        }
    }
}
