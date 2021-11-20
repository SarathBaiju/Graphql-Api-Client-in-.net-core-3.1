using Newtonsoft.Json;

namespace GraphqlApiClientDemo.Models.Graph
{
    public class EmployeeInsertMutationResponse
    {
        [JsonProperty("insert_emp_Employee_one")]
        public EmployeeDto Employee { get; set; }
    }
}
