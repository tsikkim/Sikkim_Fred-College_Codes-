using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SikkimGov.Platform.Common.External.Contracts;
using SikkimGov.Platform.Common.Models.Employee;

namespace SikkimGov.Platform.Common.External
{
    public class EmployeeAPIService : IEmployeeApiService
    {
        private readonly TimeSpan timeout;
        private HttpClient httpClient;
        private HttpClientHandler httpClientHandler;
        private readonly string employeeApiBaseUrl;
        private ILogger<EmployeeAPIService> logger;

        public EmployeeAPIService(ILogger<EmployeeAPIService> logger)
        {
            timeout =  TimeSpan.FromSeconds(90);
            employeeApiBaseUrl = NormalizeBaseUrl(ConfigurationManager.AppSettings["employeeApiBaseUrl"]);
            this.logger = logger;
        }

        public EmployeeDetails GetEmployeeDetails(string employeeCode, int officeId)
        {
            try
            {
                EnsureHttpClientCreated();

                var url = $"api/Employess?employeeCode={HttpUtility.UrlEncode(employeeCode)}&officeId={officeId}";

                using (var response = httpClient.GetAsync(url).Result)
                {
                    response.EnsureSuccessStatusCode();
                    return JsonConvert.DeserializeObject<EmployeeDetails>(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Error while getting employee details for employeeCode: {employeeCode}, officeId: {officeId}.");
                return new EmployeeDetails();
            }
        }

        public SalaryDetails GetSalaryDetails(string employeeCode, int month, int year)
        {
            try
            {
                EnsureHttpClientCreated();

                var url = $"api/Employess?EmpCode={HttpUtility.UrlEncode(employeeCode)}&Month={month}&Year={year}";

                using (var response = httpClient.GetAsync(url).Result)
                {
                    response.EnsureSuccessStatusCode();
                    return JsonConvert.DeserializeObject<SalaryDetails>(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while getting employee details for employeeCode: {employeeCode}, month: {month}, year: {year}.");
                return new SalaryDetails();
            }
        }

        private void CreateHttpClient()
        {
            httpClientHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            };

            httpClient = new HttpClient(httpClientHandler, false)
            {
                Timeout = timeout
            };

            if (!string.IsNullOrWhiteSpace(employeeApiBaseUrl))
            {
                httpClient.BaseAddress = new Uri(employeeApiBaseUrl);
            }
        }

        private void EnsureHttpClientCreated()
        {
            if (httpClient == null)
            {
                CreateHttpClient();
            }
        }

        private static string NormalizeBaseUrl(string url)
        {
            return url.EndsWith("/") ? url : url + "/";
        }
    }
}
