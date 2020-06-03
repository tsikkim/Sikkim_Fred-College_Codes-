using Newtonsoft.Json;

namespace SikkimGov.Platform.Common.Models.Employee
{
    public class FamilyMaintenance
    {
        [JsonProperty("familyMaintenanceId")]
        public int FamilyMaintenanceId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("accountHolderName")]
        public string AccountHolderName { get; set; }

        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("bankBranchId")]
        public int BankBranchId { get; set; }

        [JsonProperty("bankBranchName")]
        public string BankBranchName { get; set; }

        [JsonProperty("bankId")]
        public int BankId { get; set; }

        [JsonProperty("bankName")]
        public string BankName { get; set; }
    }
}
