using Newtonsoft.Json;

namespace SikkimGov.Platform.Common.Models.Employee
{
    public class EmployeeDetails
    {
        public EmployeeDetails()
        {
            this.Result = new Result();
        }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("alertCode")]
        public int AlertCode { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("employeeCodeType")]
        public int EmployeeCodeType { get; set; }

        [JsonProperty("result")]
        public Result Result { get; set; }
    }
}
