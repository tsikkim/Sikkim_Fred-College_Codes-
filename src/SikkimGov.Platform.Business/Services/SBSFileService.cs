using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.DataAccess.Repositories.Contracts;
using SikkimGov.Platform.Models.Domain;

namespace SikkimGov.Platform.Business.Services
{
    public class SBSFileService : ISBSFileService
    {
        private readonly ISBSPaymentRepository sbsPaymentRepository;
        private readonly ISBSReceiptRepository sbsReceiptRepository;

        public SBSFileService(ISBSPaymentRepository sbsPaymentRepository, ISBSReceiptRepository sbsReceiptRepository)
        {
            this.sbsPaymentRepository = sbsPaymentRepository;
            this.sbsReceiptRepository = sbsReceiptRepository;
        }

        public void ProcessSBSFile(SBSFileType fileType, string filePath)
        {

            var lines = File.ReadAllLines(filePath);

            if (fileType == SBSFileType.Payment)
            {
                CreateSBSPayments(lines);
            }
            else if (fileType == SBSFileType.Receipt)
            {
                CreateSBSReceipts(lines);
            }
        }

        private void CreateSBSPayments(IEnumerable<string> fileContents)
        {
            var sbsPayments = new List<SBSPayment>();

            foreach (var line in fileContents)
            {
                var lineItems = line.Split(new char[] { '|' });
                var sbsPayment = new SBSPayment();
                sbsPayment.BranchCode = lineItems[0].Trim();
                sbsPayment.BranchName = lineItems[1].Trim();
                sbsPayment.DepartmentID  = Convert.ToInt32(lineItems[2].Trim());
                sbsPayment.ChequeDate = DateTime.ParseExact(lineItems[3].Trim(), "dd/MM/yyyy", null);
                sbsPayment.PaymentDate = DateTime.ParseExact(lineItems[4].Trim(), "dd/MM/yyyy", null);
                sbsPayment.ChequeNumber = lineItems[5].Trim();
                sbsPayment.ChequeAmount = Convert.ToDecimal(lineItems[6].Trim());
                sbsPayment.PlanNonPlan = lineItems[7].Trim();
                sbsPayment.MajorHead = lineItems[8].Trim();
                sbsPayment.PAOCode = Convert.ToInt32(lineItems[9].Trim());
                sbsPayment.IsWorks = lineItems[10].Trim();
                sbsPayment.CreatedDate = DateTime.Now;

                sbsPayments.Add(sbsPayment);
            }

            this.sbsPaymentRepository.AddSBSPaymets(sbsPayments);
        }

        private void CreateSBSReceipts(IEnumerable<string> fileContents)
        {
            var sbsReceipts = new List<SBSReceipt>();

            foreach (var line in fileContents)
            {
                var lineItems = line.Split(new char[] { '|' });
                var sbsReceipt = new SBSReceipt();
                sbsReceipt.BranchCode = lineItems[0].Trim();
                sbsReceipt.BranchName = lineItems[1].Trim();
                sbsReceipt.BranchChallanNo = lineItems[2].Trim();
                sbsReceipt.TransactionDate = Convert.ToDateTime(lineItems[3].Trim());
                sbsReceipt.MajorHead = lineItems[4].Trim();
                sbsReceipt.ReceiptPayment = lineItems[5].Trim();
                sbsReceipt.Amount = Convert.ToDecimal(lineItems[6].Trim());
                sbsReceipt.CreatedDate = DateTime.Now;

                sbsReceipts.Add(sbsReceipt);
            }

            this.sbsReceiptRepository.AddSBSReceipts(sbsReceipts);
        }
    }
}
