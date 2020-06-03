using Newtonsoft.Json;

namespace SikkimGov.Platform.Common.Models.Employee
{
    public class Recovery
    {
        [JsonProperty("recoveryId")]
        public int RecoveryId { get; set; }

        [JsonProperty("recoveryTypeId")]
        public int RecoveryTypeId { get; set; }

        [JsonProperty("recoveryTypeName")]
        public string RecoveryTypeName { get; set; }

        [JsonProperty("totalAmount")]
        public int TotalAmount { get; set; }

        [JsonProperty("installmentAmount")]
        public int InstallmentAmount { get; set; }

        [JsonProperty("currentBalance")]
        public int CurrentBalance { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }
    }
}
