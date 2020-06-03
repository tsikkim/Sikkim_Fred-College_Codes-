namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IEmployeeService
    {
        void GetSalaryBill(string employeeCode, int monty, int year);

        void GetEmployeeDetails(string employeeCode, int officeId);
    }
}
