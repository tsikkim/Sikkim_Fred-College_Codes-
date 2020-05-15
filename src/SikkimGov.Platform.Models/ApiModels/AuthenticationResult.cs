namespace SikkimGov.Platform.Models.ApiModels
{
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set; }

        public bool IsSuperAdmin { get; set; }

        public bool IsAdmin { get; set; }

        public long DepartmentId { get; set; }

        public bool IsDDO { get; set; }

        public bool IsRCO { get; set; }

        public string DDOCode { get; set; }

        public string AccessToken { get; set; }
    }
}
