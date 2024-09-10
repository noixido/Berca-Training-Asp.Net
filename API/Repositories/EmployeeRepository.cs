using API.Context;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext _context;
        public EmployeeRepository(MyContext context) { _context = context; }

        public int DeleteEmployee(string emplId)
        {
            var delete = _context.Employees.Find(emplId);
            if (delete != null)
            {
                _context.Employees.Remove(delete);
                return _context.SaveChanges();
            }
            return 0;
        }

        public Employee GetEmployeeById(string emplId)
        {
            return _context.Employees.Find(emplId);
        }

        public int UpdateEmployee(Employee employee)
        {
            _context.Update(employee);
            return _context.SaveChanges();
        }

        public int AddEmployee(string firstName, string lastName,string email, string deptId)
        {
            Employee employee = new Employee();
            var lastRecord = _context.Employees.Max(last => last.Employee_Id);
            if (lastRecord == null)
            {
                employee.Employee_Id = "2024090001";
            }
            else
            {
                var lastRecordId = int.Parse(lastRecord.Substring(7));
                lastRecordId++;
                var number = lastRecordId.ToString("D4");

                DateTime now = DateTime.Now;
                employee.Employee_Id = $"{now.Year}{now.Month.ToString("D2")}{number}";
            }

            employee.FirstName = firstName;
            employee.LastName = lastName;
            employee.Email = email;
            employee.Dept_ID = deptId;

            _context.Employees.Add(employee);
            

            return _context.SaveChanges();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees
                .ToList();
        }

        public IEnumerable<EmployeeVM> EmpData()
        {
            return _context.Employees
                .Include(d => d.Department)
                .Select(d => new EmployeeVM
                {
                    Employee_Id = d.Employee_Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Dept_Name = d.Department.Dept_Name
                })
                .ToList();
        }
    }
}
