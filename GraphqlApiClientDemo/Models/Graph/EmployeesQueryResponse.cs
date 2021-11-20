using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphqlApiClientDemo.Models.Graph
{
    public class EmployeesQueryResponse
    {
        [JsonProperty("emp_Employee")]
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }

    public class EmployeeByIdQueryResponse
    {
        [JsonProperty("emp_Employee_by_pk")]
        public EmployeeDto Employee { get; set; }
    }
}
