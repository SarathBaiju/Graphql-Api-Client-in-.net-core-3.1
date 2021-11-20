using GraphQL;
using GraphqlApiClientDemo.DataAccess;
using GraphqlApiClientDemo.Models;
using GraphqlApiClientDemo.Models.Graph;
using GraphqlApiClientDemo.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphqlApiClientDemo.Services
{
    public class EmployeeService : GraphqlClientBase, IEmployeeService
    {
        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            var getAllEmployeeQuery = @"query employees{
                                              emp_Employee{
                                                id
                                                name
                                                isDeleted
                                                modifiedDate
                                                createdDate
                                              }
                                            }";

            var graphQlRequest = new GraphQLRequest()
            {
                Query = getAllEmployeeQuery
            };

            var response = await _graphQLHttpClient.SendQueryAsync<EmployeesQueryResponse>(graphQlRequest);
            ValidateGraphResponse(response);
            return response.Data.Employees;
        }

        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            var getByIdQuery = @"query employeeById($id: Int!) {
                                  emp_Employee_by_pk(id: $id) {
                                    id
                                    isDeleted
                                    modifiedDate
                                    name
                                    createdDate
                                  }
                                }";
            var graphQlRequest = new GraphQLRequest
            {
                Query = getByIdQuery,
                Variables = new
                {
                    id
                }
            };

            var response = await _graphQLHttpClient.SendQueryAsync<EmployeeByIdQueryResponse>(graphQlRequest);
            ValidateGraphResponse(response);
            return response.Data.Employee;
        }

        public async Task<int> InsertEmployee(EmployeeDto employeeDto)
        {
            var insertEmployeeMutation = @"mutation insertEmployee($employee: emp_Employee_insert_input!) {
                                                      insert_emp_Employee_one(object: $employee) {
                                                        id
                                                      }
                                                    }";

            var graphQlRequest = new GraphQLRequest
            {
                Query = insertEmployeeMutation,
                Variables = new
                {
                    employee = employeeDto
                }
            };

            var response = await _graphQLHttpClient.SendMutationAsync<EmployeeInsertMutationResponse>(graphQlRequest);
            ValidateGraphResponse(response);

            return response.Data.Employee?.Id ?? 0;
        }

        public Task<int> UpdateEmployee(EmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEmployeeById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAllEmployees()
        {
            throw new NotImplementedException();
        }

        private static void ValidateGraphResponse<T>(GraphQLResponse<T> response)
        {
            if (response.Errors != null && response.Errors.Any())
            {
                throw new Exception(string.Join(", ", response.Errors.Select(s => s.Message).ToList()));
            }
        }
    }
}
