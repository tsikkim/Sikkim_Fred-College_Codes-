using Newtonsoft.Json;

namespace SikkimGov.Platform.Common.Models.Employee
{
    public class PercentageAllowanceType
    {
        [JsonProperty("percentageAllowanceTypeId")]
        public int PercentageAllowanceTypeId { get; set; }

        [JsonProperty("percentageAllowanceName")]
        public string PercentageAllowanceName { get; set; }

        [JsonProperty("percentageAllowanceRate")]
        public int PercentageAllowanceRate { get; set; }

        [JsonProperty("percentageAllowanceAmount")]
        public int PercentageAllowanceAmount { get; set; }
    }
}
