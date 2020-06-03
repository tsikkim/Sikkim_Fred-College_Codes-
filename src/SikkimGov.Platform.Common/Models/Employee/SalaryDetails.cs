using System.Collections.Generic;

namespace SikkimGov.Platform.Common.Models.Employee
{
    public class SalaryDetails
    {
        public SalaryDetails()
        {
            this.Allowances = new Allowances();
            this.DeductionByAdjustment = new Dictionary<string, decimal>();
            this.DeductionByCheque = new Dictionary<string, decimal>();
            this.Recoveries = new Dictionary<string, decimal>();
        }

        public string EmployeeCode { get; set; }

        public string FullName { get; set; }

        public string Designation { get; set; }

        public string Section { get; set; }

        public Allowances Allowances { get; set; }

        public Dictionary<string, decimal> DeductionByCheque { get; set; }

        public Dictionary<string, decimal> DeductionByAdjustment { get; set; }

        public Dictionary<string, decimal> Recoveries { get; set; }

        public decimal GrossPay { get; set; }

        public decimal TotalDeduction { get; set; }

        public decimal NetPay { get; set; }
    }
}