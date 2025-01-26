using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using People_Power.Interfaces;
using People_Power.Repositories;
using People_Power.ViewModel;

namespace People_Power.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<IActionResult> AssignRole(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            var roles = await _roleRepository.GetAllRolesAsync();
            var model = new AssignRoleViewModel
            {
                UserId = userId,
                UserName = user.UserName,
                AvailableRoles = roles,
                CurrentRoleId = user.RoleId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleViewModel model)
        {
            var result = await _userRepository.AssignRoleAsync(model.UserId, model.RoleId);
            if (result!= null)
            {
                TempData["SuccessMessage"] = "Role assigned successfully!";
            }
            return RedirectToAction("UserList");
        }
        public async Task<IActionResult> UserList()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return View(users);
        }
    }
}
