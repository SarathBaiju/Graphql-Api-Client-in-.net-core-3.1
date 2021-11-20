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

        public async Task<int> UpdateEmployee(EmployeeDto employeeDto)
        {
            var employeeUpdateMutation = @"mutation updateEmployee($id:Int!, $employee: emp_Employee_set_input) {
                                              update_emp_Employee_by_pk(pk_columns: {id: $id}, _set: $employee) {
                                                id
                                              }
                                            }";
            var variables = new
            {
                id = employeeDto.Id,
                employee = new
                {
                    id = employeeDto.Id,
                    name = employeeDto.Name,
                    modifiedDate = DateTime.Now
                }
            };

            var graphqlRequest = new GraphQLRequest
            {
                Query = employeeUpdateMutation,
                Variables = variables
            };

            var response = await _graphQLHttpClient.SendMutationAsync<EmployeeUpdateMutationResponse>(graphqlRequest);
            ValidateGraphResponse(response);

            return response.Data.Employee?.Id ?? 0;
        }

        public async Task<bool> DeleteEmployeeById(int id)
        {
            var deleteEmployeeMutation = @"mutation deleteEmployee($id: Int) {
                                          delete_emp_Employee(where: {id: {_eq: $id}}) {
                                            affected_rows
                                          }
                                        }";
            var graphQlRequest = new GraphQLRequest
            {
                Query = deleteEmployeeMutation,
                Variables = new
                {
                    id
                }
            };

            var response = await _graphQLHttpClient.SendMutationAsync<EmployeeDeleteMutationResponse>(graphQlRequest);
            ValidateGraphResponse(response);
            return true;
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
