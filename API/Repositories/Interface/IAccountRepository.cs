using API.Models;
using API.ViewModels;

namespace API.Repositories.Interface
{
    public interface IAccountRepository
    {
        int Register(AccountVM accountVM);
        IEnumerable<empDataVM> GetAllEmployeeData();

        bool Login(LoginVM loginVM);
        empDataVM lastInsertedEmpData();

    }
}
