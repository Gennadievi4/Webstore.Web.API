using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using WebStore.Clients.Base;
using WebStore.Domain.Entitys;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        private readonly ILogger _Logger;

        public EmployeesClient(IConfiguration Configuration, ILogger<EmployeesClient> Logger) : base(Configuration, WebApi.Employees)
        {
            _Logger = Logger;
        }

        public int Add(Employee emp)
        {
            return Post(Address, emp).Content.ReadAsAsync<int>().Result;
        }

        public bool Delete(int id)
        {
            return Delete($"{Address}/{id}").IsSuccessStatusCode;
        }

        public IEnumerable<Employee> Get()
        {
            return Get<IEnumerable<Employee>>(Address);
        }

        public Employee Get(int id)
        {
            return Get<Employee>($"{Address}/{id}");
        }

        public void Update(Employee emp)
        {
            Put(Address, emp);
        }
    }
}
