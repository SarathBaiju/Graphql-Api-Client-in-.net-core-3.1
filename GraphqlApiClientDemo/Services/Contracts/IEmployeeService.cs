using GraphqlApiClientDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphqlApiClientDemo.Services.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployees();
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<int> InsertEmployee(EmployeeDto employeeDto);
        Task<int> UpdateEmployee(EmployeeDto employeeDto);
        Task<bool> DeleteEmployeeById(int id);
    }
}
