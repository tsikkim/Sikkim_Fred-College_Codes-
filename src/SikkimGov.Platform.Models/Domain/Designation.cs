using System.ComponentModel.DataAnnotations.Schema;

namespace SikkimGov.Platform.Models.Domain
{
    [Table("Designation")]
    public class Designation
    {
        [Column("DesignationId")]
        public int Id { get; set; }

        [Column("DesignationName")]
        public string Name { get; set; }
    }
}
