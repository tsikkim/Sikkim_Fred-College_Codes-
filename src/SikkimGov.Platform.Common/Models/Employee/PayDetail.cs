using Newtonsoft.Json;

namespace SikkimGov.Platform.Common.Models.Employee
{
    public class PayDetail
    {
        [JsonProperty("gradePayId")]
        public int GradePayId { get; set; }

        [JsonProperty("npaTypeId")]
        public int NpaTypeId { get; set; }

        [JsonProperty("payInPayBand")]
        public int PayInPayBand { get; set; }

        [JsonProperty("payMatrixId")]
        public int PayMatrixId { get; set; }

        [JsonProperty("basicPay")]
        public int BasicPay { get; set; }

        [JsonProperty("npaAmount")]
        public int NpaAmount { get; set; }

        [JsonProperty("effectiveBasicPay")]
        public int EffectiveBasicPay { get; set; }

        [JsonProperty("payBandId")]
        public int PayBandId { get; set; }

        [JsonProperty("payLevel")]
        public int PayLevel { get; set; }

        [JsonProperty("payCell")]
        public int PayCell { get; set; }

        [JsonProperty("extraPayPolice")]
        public bool ExtraPayPolice { get; set; }

        [JsonProperty("extraPayAmount")]
        public int ExtraPayAmount { get; set; }

        [JsonProperty("frontierAllowance")]
        public bool FrontierAllowance { get; set; }
    }
}
