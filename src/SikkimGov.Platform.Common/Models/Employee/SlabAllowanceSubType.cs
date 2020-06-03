using Newtonsoft.Json;

namespace SikkimGov.Platform.Common.Models.Employee
{
    public class SlabAllowanceSubType
    {
        [JsonProperty("slabAllowanceSubTypeId")]
        public int SlabAllowanceSubTypeId { get; set; }

        [JsonProperty("slabAllowanceSubTypeName")]
        public string SlabAllowanceSubTypeName { get; set; }

        [JsonProperty("slabAllowanceTypeId")]
        public int SlabAllowanceTypeId { get; set; }

        [JsonProperty("slabAllowanceTypeName")]
        public string SlabAllowanceTypeName { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
}
