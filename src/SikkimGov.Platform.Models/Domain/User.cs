using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SikkimGov.Platform.Models.Domain
{
    [Table("UserInfo")]
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string EmailId { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserType UserType { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastLoginDate { get; set; }
    }
}
