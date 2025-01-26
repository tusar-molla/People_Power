using People_Power.Models;

namespace People_Power.ViewModel
{
    public class AssignRoleViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public IEnumerable<Role> AvailableRoles { get; set; }
        public int CurrentRoleId { get; set; }
    }
}
