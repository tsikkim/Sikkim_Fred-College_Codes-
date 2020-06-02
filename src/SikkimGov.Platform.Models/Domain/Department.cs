using System.ComponentModel.DataAnnotations.Schema;

namespace SikkimGov.Platform.Models.Domain
{
    [Table("Department")]
    public class Department
    {
        [Column("DepartmentId")]
        public int Id { get; set; }

        [Column("DepartmentName")]
        public string Name { get; set; }
    }
}
