using Newtonsoft.Json;

namespace SikkimGov.Platform.Common.Models.Employee
{
    public class FixedAllowanceType
    {
        [JsonProperty("fixedAllowanceTypeId")]
        public int FixedAllowanceTypeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
}
