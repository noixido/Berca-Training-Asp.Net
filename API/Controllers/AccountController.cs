using API.Models;
using API.Repositories;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private AccountRepository _accountRepository;
        public AccountController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        public IActionResult Register(AccountVM accountVM)
        {
            try
            {
                var register = _accountRepository.Register(accountVM);
                if (register > 0)
                {
                    var data = _accountRepository.lastInsertedEmpData();
                    return Ok(new
                    {
                        status = StatusCodes.Status200OK,
                        message = "Data Berhasil Dimasukkan",
                        data = (object)data,
                    });
                }
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "Data Gagal Dimasukkan",
                    data = (object)false,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = ex.Message,
                });
            }
        }

        [HttpGet("EmpData")]
        public IActionResult GetAllEmployeeData()
        {
            var emp = _accountRepository.GetAllEmployeeData();
            if (emp == null)
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
                data = (object)emp,
            });
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginVM loginVM)
        {
            try
            {
                var user = _accountRepository.Login(loginVM);
                if (!user)
                {
                    return BadRequest(new
                    {
                        status = StatusCodes.Status400BadRequest,
                        message = "Login Failed!",
                        data = (object)false,
                    });
                }
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Login Success!",
                    data = (object)user,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = ex.Message,
                });
            }
        }
    }
}
