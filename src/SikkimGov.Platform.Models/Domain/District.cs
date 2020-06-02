using System.ComponentModel.DataAnnotations.Schema;

namespace SikkimGov.Platform.Models.Domain
{
    [Table("District")]
    public class District
    {
        [Column("DistrictId")]
        public int Id { get; set; }

        [Column("DistrictName")]
        public string Name { get; set; }
    }
}
