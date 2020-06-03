using Newtonsoft.Json;

namespace SikkimGov.Platform.Common.Models.Employee
{
    public class Loan
    {
        [JsonProperty("loanId")]
        public int LoanId { get; set; }

        [JsonProperty("bankBranchId")]
        public int BankBranchId { get; set; }

        [JsonProperty("bankBranchName")]
        public string BankBranchName { get; set; }

        [JsonProperty("bankId")]
        public int BankId { get; set; }

        [JsonProperty("bankName")]
        public string BankName { get; set; }

        [JsonProperty("ifscCode")]
        public string IfscCode { get; set; }

        [JsonProperty("bankAccountNumber")]
        public string BankAccountNumber { get; set; }

        [JsonProperty("loanInstallmentAmount")]
        public int LoanInstallmentAmount { get; set; }

        [JsonProperty("numberOfInstallment")]
        public int NumberOfInstallment { get; set; }
    }
}
