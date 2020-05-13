using System;

namespace SikkimGov.Platform.Models.DomainModels
{
    public class User
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsLoggedIn { get; set; }

        public DateTime LastLoginDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public long LastSchoolNumber { get; set; }

        public byte Step { get; set; }

        public byte UserType { get; set; }

        public bool IsAdmin { get; set; }

        public long? DepartmentId { get; set; }

        public bool IsDDO { get; set; }

        public bool IsSuper { get; set; }

        public string EmailId { get; set; }

        public bool IsRCO { get; set; }

        public string DDOCode { get; set; }
    }
}
