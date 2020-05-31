using System.Runtime.Intrinsics.X86;

namespace SikkimGov.Platform.Models.ApiModels
{
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }

        public long UserId { get; set; }

        public string EmailId { get; set; }

        public int DepartmentId { get; set; }

        public int DistrictId { get; set; }

        public int DesignationId { get; set; }

        public bool IsSuperAdmin { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsDDO { get; set; }

        public bool IsRCO { get; set; }

        public string AccessToken { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
