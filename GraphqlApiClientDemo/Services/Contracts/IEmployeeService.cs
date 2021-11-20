using GraphqlApiClientDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        Task<bool> DeleteAllEmployees();
    }
}
