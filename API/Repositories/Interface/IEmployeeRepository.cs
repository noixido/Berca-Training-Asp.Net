using API.Models;
using API.ViewModels;

namespace API.Repositories.Interface
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        int AddEmployee(string firstName, string lastName, string email, string deptId);
        Employee GetEmployeeById(string emplId);
        int UpdateEmployee(Employee employee);
        int DeleteEmployee(string emplId);

        IEnumerable<EmployeeVM> EmpData();
    }
}
