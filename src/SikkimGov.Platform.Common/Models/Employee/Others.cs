using Newtonsoft.Json;

namespace SikkimGov.Platform.Common.Models.Employee
{
    public class Others
    {
        [JsonProperty("otherId")]
        public int OtherId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
}
