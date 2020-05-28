using System.ComponentModel.DataAnnotations.Schema;

namespace SikkimGov.Platform.Models.Domain
{
    [Table("District")]
    public class District
    {
        public int DistrictId { get; set; }

        public string DistrictName { get; set; }
    }
}
