namespace People_Power.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; } // Store hashed passwords
        public string Email { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
