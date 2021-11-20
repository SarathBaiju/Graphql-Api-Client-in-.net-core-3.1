using GraphqlApiClientDemo.Models;
using GraphqlApiClientDemo.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GraphqlApiClientDemo.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet]
        [Route("employees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                return Ok(await _employeeService.GetEmployees());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("employee/{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeById(id);

                if (employee == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Employee not found with Id: {id}");
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("employee/add")]
        public async Task<IActionResult> InsertEmployee([FromBody] EmployeeViewModel employeeVM)
        {
            try
            {
                var employee = new EmployeeDto
                {
                    Id = employeeVM.Id,
                    Name = employeeVM.Name,
                    CreatedDate = DateTime.Now
                };
                return Ok(await _employeeService.InsertEmployee(employee));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("employee/update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeViewModel employeeVM)
        {
            try
            {
                var employee = new EmployeeDto
                {
                    Id = employeeVM.Id,
                    Name = employeeVM.Name
                };
                return Ok(await _employeeService.UpdateEmployee(employee));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("employee/delete/{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            try
            {
                return Ok(await _employeeService.DeleteEmployeeById(id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
