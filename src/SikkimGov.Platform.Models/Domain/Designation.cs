using System.ComponentModel.DataAnnotations.Schema;

namespace SikkimGov.Platform.Models.Domain
{
    [Table("Designation")]
    public class Designation
    {
        public int DesignationId { get; set; }

        public string DesinationName { get; set; }
    }
}
