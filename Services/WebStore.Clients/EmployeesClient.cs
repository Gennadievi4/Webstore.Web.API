using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WebStore.Clients.Values.Base;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.Clients
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        private readonly ILogger _Logger;

        public EmployeesClient(IConfiguration Configuration, ILogger Logger) : base(Configuration, "api/employees")
        {
            _Logger = Logger;
        }

        public int Add(Employee emp)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> Get()
        {
            throw new NotImplementedException();
        }

        public Employee Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee emp)
        {
            throw new NotImplementedException();
        }
    }
}
