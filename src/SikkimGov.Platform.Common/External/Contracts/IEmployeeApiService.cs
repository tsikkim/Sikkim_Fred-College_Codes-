using SikkimGov.Platform.Common.Models.Employee;

namespace SikkimGov.Platform.Common.External.Contracts
{
    public interface IEmployeeApiService
    {
        SalaryDetails GetSalaryDetails(string employeeCode, int month, int year);

        EmployeeDetails GetEmployeeDetails(string emplyeeCode, int officeId);
    }
}
