using People_Power.Models;

namespace People_Power.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        IEnumerable<Department> GetAllDepartments();
    }
}
