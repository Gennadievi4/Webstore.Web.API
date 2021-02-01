using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controller
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;
        private readonly ILogger<EmployeesApiController> _Logger;

        public EmployeesApiController(IEmployeesData EmployeesData, ILogger<EmployeesApiController> Logger)
        {
            _EmployeesData = EmployeesData;
            _Logger = Logger;
        }

        [HttpPost]
        public int Add(Employee emp)
        {
            if (!ModelState.IsValid)
            {
                _Logger.LogInformation("Ошибка модели данных при добавлении нового сотрудника {0} {1} {2}", emp.LastName, emp.FirstName, emp.Patronymic);
                return 0;
            }

            _Logger.LogInformation("Добавление сотрудника {0} {1} {2}", emp.LastName, emp.FirstName, emp.Patronymic);
            var id = _EmployeesData.Add(emp);

            if (id > 0)
                _Logger.LogInformation("Сотрудник [id:{0}] {1} {2} {3} добавлен успешно",
                    emp.Id, emp.LastName, emp.FirstName, emp.Patronymic);
            else
                _Logger.LogWarning("Ошибка при добавлении сотрудника {0} {1} {2}",
                    emp.Id, emp.LastName, emp.FirstName, emp.Patronymic);

            return id;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var result = _EmployeesData.Delete(id);
            if (result)
                _Logger.LogInformation("Сотрудник с id:{0} успешно удалён", id);
            else
                _Logger.LogWarning("Ошибка при попытке удаления сотрудника с id:{0}", id);
            return _EmployeesData.Delete(id);
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _EmployeesData.Get();
        }

        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return _EmployeesData.Get(id);
        }

        [HttpPut]
        public void Update(Employee emp)
        {
            _EmployeesData.Update(emp);
        }
    }
}
