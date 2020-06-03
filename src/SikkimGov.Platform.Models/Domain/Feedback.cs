using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SikkimGov.Platform.Models.Domain
{
    [Table("Feedback")]
    public class Feedback
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string EmailID { get; set; }

        public string ContactNumber { get; set; }

        public string Comments { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
