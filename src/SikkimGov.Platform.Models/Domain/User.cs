using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SikkimGov.Platform.Models.Domain
{
    [Table("UserInfo")]
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string EmailID { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserType UserType { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime? LastLoginDate { get; set; }
    }
}
