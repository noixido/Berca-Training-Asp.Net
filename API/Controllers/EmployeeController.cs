using API.Models;
using API.Repositories;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private EmployeeRepository _employeeRepository;
        public EmployeeController(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = _employeeRepository.GetAllEmployees();
            if (employees.Any())
            {
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Data Ditemukan",
                    data = (object)employees,
                });
            }
            else
            {
                return NotFound(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "Data Tidak Ditemukan",
                    data = (object)null,
                });
            }
        }

        [HttpPost]
        public IActionResult AddEmployee(string firstName, string lastName, string email, string deptId)
        {
            var addEmployee = _employeeRepository.AddEmployee(firstName, lastName, email, deptId);
            if (addEmployee > 0)
            {
                Employee empl = new Employee();
                empl.FirstName = firstName;
                empl.LastName = lastName;
                empl.Email = email;
                empl.Dept_ID = deptId;

                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Data Berhasil Dimasukkan",
                    data = (object)empl,
                });
            }
            return BadRequest(new
            {
                status = StatusCodes.Status400BadRequest,
                message = "Data Gagal Dimasukkan",
                data = (object)false,
            });
        }

        [HttpGet("{emplId}")]
        public IActionResult GetEmployeeById(string emplId)
        {
            var employee = _employeeRepository.GetEmployeeById(emplId);
            if (employee == null)
            {
                return NotFound(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "Data Tidak Ditemukan",
                    data = (object)null,
                });
            }
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Data Ditemukan",
                data = (object)employee,
            });
        }

        [HttpDelete("{emplId}")]
        public IActionResult DeleteDepartment(string emplId)
        {
            int res = _employeeRepository.DeleteEmployee(emplId);
            if (res > 0)
            {
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Data Berhasil Dihapus",
                    data = (object)true,
                });
            }
            return NotFound(new
            {
                status = StatusCodes.Status404NotFound,
                message = "Data Tidak Ditemukan",
                data = (object)null,
            });
        }

        [HttpPut("{emplId}")]
        public IActionResult UpdateDepartment(string emplId, [FromBody] Employee employee)
        {
            var checkRecordId = _employeeRepository.GetEmployeeById(emplId);
            if (checkRecordId == null)
            {
                return NotFound(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "Data Tidak Ditemukan",
                    data = (object)null,
                });
            }

            checkRecordId.FirstName = employee.FirstName;
            checkRecordId.LastName = employee.LastName;
            checkRecordId.Dept_ID = employee.Dept_ID;

            int res = _employeeRepository.UpdateEmployee(checkRecordId);

            if (res > 0)
            {
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Data Berhasil Diperbaharui",
                    data = (object)checkRecordId,
                });
            }
            return NotFound(new
            {
                status = StatusCodes.Status404NotFound,
                message = "Data Tidak Ditemukan",
                data = (object)null,
            });
        }

        [HttpGet("EmpData")]
        public IActionResult EmpData()
        {
            var emp = _employeeRepository.EmpData();
            if (emp != null)
            {
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Data Ditemukan",
                    data = (object)emp,
                });
            }
            return NotFound(new
            {
                status = StatusCodes.Status404NotFound,
                message = "Data Tidak Ditemukan",
                data = (object)null,
            });
        }
    }
}
