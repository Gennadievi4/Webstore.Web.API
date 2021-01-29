using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using WebStore.Clients.Values.Base;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.Clients
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        private readonly ILogger _Logger;

        public EmployeesClient(IConfiguration Configuration, ILogger<EmployeesClient> Logger) : base(Configuration, "api/employees")
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
