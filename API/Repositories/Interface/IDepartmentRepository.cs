using API.Models;

namespace API.Repositories.Interface
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartments();
        Department GetDepartmentsById(string deptId);
        int AddDepartment(Department department);
        int UpdateDepartment(Department department);
        int DeleteDepartment(string deptId);
    }
}
