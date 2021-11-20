using Newtonsoft.Json;

namespace GraphqlApiClientDemo.Models.Graph
{
    public class EmployeeInsertMutationResponse
    {
        [JsonProperty("insert_emp_Employee_one")]
        public EmployeeDto Employee { get; set; }
    } 
    
    public class EmployeeUpdateMutationResponse
    {
        [JsonProperty("update_emp_Employee_by_pk")]
        public EmployeeDto Employee { get; set; }
    }

    public class EmployeeDeleteMutationResponse
    {
        [JsonProperty("delete_emp_Employee")]
        public AffectedRowsResponse Employee { get; set; }
    }
}
