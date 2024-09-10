using API.Context;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyContext _context;
        public AccountRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<empDataVM> GetAllEmployeeData()
        {
            return _context.Employees
                .Include(a => a.Account)
                .Include(d => d.Department)
                .Select(a => new empDataVM
                {
                    NIK = a.Employee_Id,
                    Email = a.Email,
                    FullName = a.FirstName+" "+a.LastName,
                    Username = a.Account.Username,
                    DeptName = a.Department.Dept_Name,
                });
        }

        public bool Login(LoginVM loginVM)
        {
            var username = _context.Accounts.Include(e => e.Employee).FirstOrDefault(a => a.Username == loginVM.Username || a.Employee.Email == loginVM.Username);
            if (username == null)
            {
                throw new Exception("Username/Email is Invalid");
            }
            //var checkPass = username.Password == loginVM.Password;
            var checkPass = BCrypt.Net.BCrypt.Verify(loginVM.Password, username.Password);
            if (!checkPass)
            {
                throw new Exception("Try Again!");
            }
            return true;
        }

        public int Register(AccountVM? accountVM)
        {
            Employee employee = new Employee();
            DateTime now = DateTime.Now;

            var lastRecord = _context.Employees.Max(x => x.Employee_Id);
            if (lastRecord == null)
            {
                //employee.Employee_Id = "2024090001";
                employee.Employee_Id = $"{now.Year}{now.Month.ToString("D2")}0001";

            }
            else
            {
                var lastRecordId = int.Parse(lastRecord.Substring(7));
                lastRecordId++; //2
                var number = lastRecordId.ToString("D4"); //0002

                employee.Employee_Id = $"{now.Year}{now.Month.ToString("D2")}{number}";
            }

            employee.FirstName = accountVM.FirstName;
            employee.LastName = accountVM.LastName;
            employee.Email = accountVM.Email;

            if(_context.Employees.Any(e => e.Email == accountVM.Email))
            {
                throw new Exception("Email sudah terdaftar, silakan gunakan email lain!");
            }

            employee.Dept_ID = accountVM.Dept_ID;

            _context.Employees.Add(employee);

            Account account = new Account();

            account.Account_ID = employee.Employee_Id;
            accountVM.Username = $"{accountVM.FirstName.ToLower()}.{accountVM.LastName.ToLower()}";

            var baseUsername = accountVM.Username;
            var similarCount = _context.Accounts.Count(x => x.Username.StartsWith(baseUsername));
            if(similarCount == 0)
            {
                account.Username = baseUsername;
            }
            else
            {
                account.Username = baseUsername + similarCount.ToString("D3");
            }

            //account.Password = "12345";

            var pass = "12345";
            var generateSalt = BCrypt.Net.BCrypt.GenerateSalt(13);
            account.Password = BCrypt.Net.BCrypt.HashPassword(pass, generateSalt);

            _context.Accounts.Add(account);

            return _context.SaveChanges();
        }

        public empDataVM lastInsertedEmpData()
        {
            var lastInserted = _context.Employees
                .Include(d => d.Department)
                .Include(a => a.Account)
                .OrderByDescending(x => x.Employee_Id)
                .FirstOrDefault();
            if(lastInserted == null)
            {
                return null;
            }
            return new empDataVM
            {
                NIK = lastInserted.Employee_Id,
                Email = lastInserted.Email,
                FullName = $"{lastInserted.FirstName} {lastInserted.LastName}",
                Username = lastInserted.Account.Username,
                DeptName = lastInserted.Department.Dept_Name
            };
        }
    }
}
