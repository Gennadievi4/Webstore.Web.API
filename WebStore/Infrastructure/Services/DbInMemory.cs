using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public class DbInMemory : IEmployeesData
    {
        public List<Employee> _Employees = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 1 },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Пётр", Patronymic = "Петрович", Age = 3 },
            new Employee { Id = 3, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 2 }
        };

        public int Add(Employee emp)
        {
            if (emp is null)
                throw new ArgumentNullException(nameof(emp));

            if (_Employees.Contains(emp))
                return emp.Id;

            emp.Id = _Employees
                .Select(item => item.Id)
                .DefaultIfEmpty()
                .Max() + 1;

            _Employees.Add(emp);

            return emp.Id;
        }

        public bool Delete(int id)
        {
            var item = Get(id);
            if (item is null) return false;
            return _Employees.Remove(item);
        }

        public IEnumerable<Employee> Get() => _Employees;

        public Employee Get(int id) => _Employees.FirstOrDefault(x => x.Id == id);

        public void Update(Employee emp)
        {
            if (emp is null)
                throw new ArgumentNullException(nameof(emp));

            if (_Employees.Contains(emp))
                return;

            var db_item = Get(emp.Id);
            if (db_item is null)
                return;

            db_item.LastName = emp.LastName;
            db_item.FirstName = emp.FirstName;
            db_item.Patronymic = emp.Patronymic;
            db_item.Age = emp.Age;
        }
    }
}
