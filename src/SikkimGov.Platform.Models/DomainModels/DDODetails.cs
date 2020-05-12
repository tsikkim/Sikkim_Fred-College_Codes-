namespace SikkimGov.Platform.Models.DomainModels
{
    public class DDODetails : DDOBase
    {
        public int DesignationId { get; set; }

        public string DesignationName { get; set; }

        public int DistrictId { get; set; }

        public string DistrictName { get; set; }
    }
}
