using System.Globalization;

namespace SikkimGov.Platform.Models.DomainModels
{
    public class UserDetails
    {
        public int Id { get; set; }

        public string EmailId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DepartmentName { get; set; }
        
        public string RegistrationType { get; set; }
        
        public string Name { get; set; }
        
        public string DDOCode { get; set; }
        
        public bool IsDDOUser { get; set; }
        
        public bool IsRCOUser { get; set; }
        
        public bool IsAdmin { get; set; }
        
        public bool IsSuperAdmin { get; set; }

        public string UserType { get; set; }
    }
}