using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class DepartmentController : Controller
    {
        private DepartmentRepository _departmentRepository;
        public DepartmentController(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            var departments = _departmentRepository.GetAllDepartments();
            if (departments.Count() != 0)
            {
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Data Ditemukan",
                    data = (object)departments,
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
        public IActionResult AddDepartment(Department department)
        {
            var addDepartment = _departmentRepository.AddDepartment(department);
            if (addDepartment > 0)
            {
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Data Berhasil Dimasukkan",
                    data = (object)true,
                });
            }
            return BadRequest(new
            {
                status = StatusCodes.Status400BadRequest,
                message = "Data Gagal Dimasukkan",
                data = (object)false,
            });

        }

        [HttpGet("{deptId}")]
        public IActionResult GetDepartmentById(string deptId)
        {
            var department = _departmentRepository.GetDepartmentsById(deptId);
            if (department == null)
            {
                //return NotFound("Data Tidak Ditemukan");
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
                data = (object)department,
            });
        }

        [HttpPut("{deptId}")]
        public IActionResult UpdateDepartment(string deptId, [FromBody] Department department)
        {
            var checkRecordId = _departmentRepository.GetDepartmentsById(deptId);
            if(checkRecordId == null)
            {
                return NotFound(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "Data Tidak Ditemukan",
                    data = (object)null,
                });
            }

            checkRecordId.Dept_Initial = department.Dept_Initial;
            checkRecordId.Dept_Name = department.Dept_Name;

            int result = _departmentRepository.UpdateDepartment(checkRecordId);
            
            if (result > 0)
            {
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Data Berhasil Diperbaharui",
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

        [HttpDelete]
        public IActionResult DeleteDepartment(string? deptId)
        {
            int result = _departmentRepository.DeleteDepartment(deptId);
            if (result > 0)
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
    }
}
