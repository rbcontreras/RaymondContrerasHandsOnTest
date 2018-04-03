using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RaymondContreras_HandsOnTest.Models;
using RaymondContreras_HandsOnTest.Services.Interfaces;
using RaymondContreras_HandsOnTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace RaymondContreras_HandsOnTest.Services
{
    public class EmployeeService : IEmployeeService
    {
        #region private (can create separate/common class for this area to make it re-usable)

        private string ApiUrl { get { return this._config["BaseUrl"]; } }

        private string EmployeeEndpoint { get { return this._config["EmployeeEndpoint"]; } }

        private IConfiguration _config;

        private readonly IMapper _mapper;

        #endregion

        public EmployeeService(IConfiguration config, IMapper mapper)
        {
            this._config = config;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get all employee
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EmployeeViewModel>> GetAllEmployee()
        {
            IEnumerable<EmployeeViewModel> employeeList = null;
            using (var client = new HttpClient(new HttpClientHandler{ UseCookies = false }))
            {
                client.BaseAddress = new Uri(ApiUrl);
                HttpResponseMessage response = await client.GetAsync(EmployeeEndpoint);
                if (response.IsSuccessStatusCode)
                {
                    var contents = await response.Content.ReadAsStringAsync();
                    var empDto = JsonConvert.DeserializeObject<IEnumerable<Employee>>(contents);
                    employeeList = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(empDto)
                        .ToList()
                        .ComputeAnnualSalary();
                }
            }
            return employeeList;
        }

        /// <summary>
        /// Get Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EmployeeViewModel> GetAllEmployeeById(int id)
        {
            var empList = GetAllEmployee()
                            .Result
                            .ToList()
                            .FirstOrDefault(_ => _.Id == id);

            return await Task.FromResult(empList);
        }        
    }

    /// <summary>
    /// Compute salary logic extention method
    /// </summary>
    internal static class ComputeSalary
    {
        /// <summary>
        /// Constant variable for Hourly Salary type for employee from API source
        /// </summary>
        private const string HourlySalary = "HourlySalaryEmployee";

        /// <summary>
        /// Constant variable for Monthly Salary type for employee from API source
        /// </summary>
        private const string MonthlySalary = "MonthlySalaryEmployee";

        internal static IEnumerable<EmployeeViewModel> ComputeAnnualSalary(this List<EmployeeViewModel> employees)
        {
            employees.ForEach(delegate (EmployeeViewModel emp)
            {
                if (emp.ContractTypeName.Equals(HourlySalary, StringComparison.CurrentCultureIgnoreCase))
                    emp.AnnualSalary = 120 * emp.HourlySalary * 12;

                else if (emp.ContractTypeName.Equals(MonthlySalary, StringComparison.CurrentCultureIgnoreCase))
                    emp.AnnualSalary = emp.HourlySalary * 12;
            });

            return employees;
        }
    }
}
