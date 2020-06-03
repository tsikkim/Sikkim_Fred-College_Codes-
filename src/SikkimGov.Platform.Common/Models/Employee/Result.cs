using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SikkimGov.Platform.Common.Models.Employee
{
    public class Result
    {
        public Result()
        {
            this.PayDetail = new PayDetail();
        }

        [JsonProperty("timeScaleEmployeeId")]
        public int TimeScaleEmployeeId { get; set; }

        [JsonProperty("employeeCode")]
        public string EmployeeCode { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("communityId")]
        public int CommunityId { get; set; }

        [JsonProperty("reservationCategoryId")]
        public int ReservationCategoryId { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("dateOfJoining")]
        public DateTime DateOfJoining { get; set; }

        [JsonProperty("dateOfRetirement")]
        public DateTime DateOfRetirement { get; set; }

        [JsonProperty("religionId")]
        public int ReligionId { get; set; }

        [JsonProperty("aadharNumber")]
        public string AadharNumber { get; set; }

        [JsonProperty("virtualIdentificationNumber")]
        public string VirtualIdentificationNumber { get; set; }

        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("bankBranchId")]
        public int BankBranchId { get; set; }

        [JsonProperty("bankId")]
        public int BankId { get; set; }

        [JsonProperty("emailId")]
        public string EmailId { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("departmentId")]
        public int DepartmentId { get; set; }

        [JsonProperty("designationId")]
        public int DesignationId { get; set; }

        [JsonProperty("districtId")]
        public int DistrictId { get; set; }

        [JsonProperty("employeeStatusId")]
        public int EmployeeStatusId { get; set; }

        [JsonProperty("employeeTypeId")]
        public int EmployeeTypeId { get; set; }

        [JsonProperty("officeId")]
        public int OfficeId { get; set; }

        [JsonProperty("probation")]
        public int Probation { get; set; }

        [JsonProperty("payRevision")]
        public int PayRevision { get; set; }

        [JsonProperty("salaryType")]
        public int SalaryType { get; set; }

        [JsonProperty("sectionId")]
        public int SectionId { get; set; }

        [JsonProperty("serviceTypeId")]
        public int ServiceTypeId { get; set; }

        [JsonProperty("appointmentOrderDate")]
        public DateTime AppointmentOrderDate { get; set; }

        [JsonProperty("appointmentOrderNumber")]
        public string AppointmentOrderNumber { get; set; }

        [JsonProperty("payDetail")]
        public PayDetail PayDetail { get; set; }

        [JsonProperty("employeeClass")]
        public int EmployeeClass { get; set; }

        [JsonProperty("vehicleAttached")]
        public bool VehicleAttached { get; set; }

        [JsonProperty("numberOfVehicle")]
        public int NumberOfVehicle { get; set; }

        [JsonProperty("governmentAccomodation")]
        public bool GovernmentAccomodation { get; set; }

        [JsonProperty("accomodationClass")]
        public int AccomodationClass { get; set; }

        [JsonProperty("isPostedOutsideSikkim")]
        public bool IsPostedOutsideSikkim { get; set; }

        [JsonProperty("isPersonalPay")]
        public bool IsPersonalPay { get; set; }

        [JsonProperty("personalPayAmount")]
        public int PersonalPayAmount { get; set; }

        [JsonProperty("isTexExempted")]
        public bool IsTexExempted { get; set; }

        [JsonProperty("coiNumber")]
        public string CoiNumber { get; set; }

        [JsonProperty("panNumber")]
        public string PanNumber { get; set; }

        [JsonProperty("volumeNumber")]
        public string VolumeNumber { get; set; }

        [JsonProperty("specialPayAllowance")]
        public int SpecialPayAllowance { get; set; }

        [JsonProperty("percentageAllowanceTypes")]
        public IList<PercentageAllowanceType> PercentageAllowanceTypes { get; set; }

        [JsonProperty("fixedAllowanceTypes")]
        public IList<FixedAllowanceType> FixedAllowanceTypes { get; set; }

        [JsonProperty("slabAllowanceSubTypes")]
        public IList<SlabAllowanceSubType> SlabAllowanceSubTypes { get; set; }

        [JsonProperty("loans")]
        public IList<Loan> Loans { get; set; }

        [JsonProperty("insurances")]
        public IList<Insurance> Insurances { get; set; }

        [JsonProperty("familyMaintenances")]
        public IList<FamilyMaintenance> FamilyMaintenances { get; set; }

        [JsonProperty("others")]
        public IList<Others> Others { get; set; }

        [JsonProperty("deductionsByAdjustment")]
        public IList<DeductionsByAdjustment> DeductionsByAdjustment { get; set; }

        [JsonProperty("recoveries")]
        public IList<Recovery> Recoveries { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }
    }
}
