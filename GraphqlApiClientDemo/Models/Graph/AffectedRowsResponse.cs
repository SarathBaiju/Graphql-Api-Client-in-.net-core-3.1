using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphqlApiClientDemo.Models.Graph
{
    public class AffectedRowsResponse
    {
        [JsonProperty("affected_rows")]
        public int AffectedRows { get; set; }
    }
}
