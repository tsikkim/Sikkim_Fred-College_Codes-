using Newtonsoft.Json;

namespace SikkimGov.Platform.Common.Models.Employee
{
    public class Insurance
    {
        [JsonProperty("insuranceId")]
        public int InsuranceId { get; set; }

        [JsonProperty("insuranceCompanyId")]
        public int InsuranceCompanyId { get; set; }

        [JsonProperty("insuranceCompanyName")]
        public string InsuranceCompanyName { get; set; }

        [JsonProperty("insuranceCompanyBranchId")]
        public int InsuranceCompanyBranchId { get; set; }

        [JsonProperty("insuranceCompanyBranchName")]
        public string InsuranceCompanyBranchName { get; set; }

        [JsonProperty("paCode")]
        public int PaCode { get; set; }

        [JsonProperty("policyNumber")]
        public string PolicyNumber { get; set; }

        [JsonProperty("premiumAmount")]
        public int PremiumAmount { get; set; }
    }
}
