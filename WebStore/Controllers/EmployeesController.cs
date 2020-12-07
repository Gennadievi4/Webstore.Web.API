using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("Users")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _Employees;
        public EmployeesController(IEmployeesData employees)
        {
            _Employees = employees;
        }

        //[Route("All")]
        public IActionResult Index()
        {
            var employees = _Employees.Get();
            return View(employees);
        }

        //[Route("id-{id}")]
        public IActionResult Details(int id)
        {
            var employee = _Employees.Get(id);
            if (employee is not null)
                return View(employee);

            return NotFound();
        }

        public IActionResult Edit(int id)
        {
            if (id < 0)
                return BadRequest();

            var emp = _Employees.Get(id);
            if (emp is null)
                return NotFound();

            return View(new EmployeesViewModel
            {
                Id = emp.Id,
                LastName = emp.LastName,
                FirstName = emp.FirstName,
                Patronymic = emp.Patronymic,
                Age = emp.Age,
            });
        }
    }
}
