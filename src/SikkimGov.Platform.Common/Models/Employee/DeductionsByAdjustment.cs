using Newtonsoft.Json;

namespace SikkimGov.Platform.Common.Models.Employee
{
    public class DeductionsByAdjustment
    {
        [JsonProperty("deductionByAdjustmentId")]
        public int DeductionByAdjustmentId { get; set; }

        [JsonProperty("deductionByAdjustmentTypeId")]
        public int DeductionByAdjustmentTypeId { get; set; }

        [JsonProperty("deductionByAdjustmentTypeName")]
        public string DeductionByAdjustmentTypeName { get; set; }

        [JsonProperty("deductionAmount")]
        public int DeductionAmount { get; set; }

        [JsonProperty("adjustmentHead")]
        public string AdjustmentHead { get; set; }
    }
}
