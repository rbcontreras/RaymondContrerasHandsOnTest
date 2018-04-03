using RaymondContreras_HandsOnTest.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RaymondContreras_HandsOnTest.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeViewModel>> GetAllEmployee();

        Task<EmployeeViewModel> GetAllEmployeeById(int id);
    }
}
