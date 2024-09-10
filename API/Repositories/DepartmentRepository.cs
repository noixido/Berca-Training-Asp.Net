using API.Context;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MyContext _myContext;
        public DepartmentRepository(MyContext myContext)
        {
            _myContext = myContext;
        }
        public int AddDepartment(Department department)
        {
            var lastRecord = _myContext.Departments.Max(last => last.Dept_ID);
            if (lastRecord == null)
            {
                department.Dept_ID = "D001";
            }
            else
            {
            var lastRecordId = int.Parse(lastRecord.Substring(1));
                lastRecordId++;
                var number = lastRecordId.ToString("D3");
                var customId = "D" + number;

                department.Dept_ID = customId;
            }

            _myContext.Departments.Add(department);
            return _myContext.SaveChanges();
        }

        public int DeleteDepartment(string deptId)
        {
            var departmentToDelete = _myContext.Departments.Find(deptId);
            if (departmentToDelete != null)
            {
                _myContext.Departments.Remove(departmentToDelete);
                return _myContext.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _myContext.Departments.ToList();
        }

        public Department GetDepartmentsById(string deptId)
        {
            return _myContext.Departments.Find(deptId);
        }

        public int UpdateDepartment(Department department)
        {
            _myContext.Entry(department).State = EntityState.Modified;    
            return _myContext.SaveChanges();
        }
    }
}
