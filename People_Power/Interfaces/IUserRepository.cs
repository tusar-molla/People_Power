using People_Power.Models;

namespace People_Power.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> IsEmailExistsAsync(string email);
        Task<bool> CreateUserAsync(User user);
        Task<bool> IsUserNameExistsAsync(string userName);
        Task<User> GetUserByIdAsync(int id);
        Task<User> AssignRoleAsync(int userId, int roleId);
    }
}
