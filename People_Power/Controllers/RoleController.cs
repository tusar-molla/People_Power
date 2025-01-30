using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using People_Power.Interfaces;
using People_Power.Models;

namespace People_Power.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<IActionResult> RoleList()
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            return View(roles);
        }
        [HttpGet]
        public IActionResult CreateRoles()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoles(Role role)
        {
            if (ModelState.IsValid)
            {
                await _roleRepository.AddRoleAsync(role);
                TempData["SuccessMessage"] = "Role created successfully!";
            }
            return RedirectToAction("RoleList");
        }
    }
}
