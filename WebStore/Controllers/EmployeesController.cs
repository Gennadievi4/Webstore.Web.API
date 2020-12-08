using Microsoft.AspNetCore.Mvc;
using System;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("Users")]
    public class EmployeesController : Controller
    {
        private IEmployeesData _Employees;
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

        #region Edit

        [HttpPost]
        public IActionResult Create() => View("Index", new EmployeesViewModel());

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id < 0)
                return BadRequest();

            if (id is null)
                return View(new EmployeesViewModel());

            var emp = _Employees.Get((int)id);
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

        [HttpPost]
        public IActionResult Edit(EmployeesViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model is null)
                throw new ArgumentNullException(nameof(model));

            var employee = new Employee
            {
                Id = model.Id,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Patronymic = model.Patronymic,
                Age = model.Age,
            };

            if (employee.Id == 0)
                _Employees.Add(employee);
            else
                _Employees.Update(employee);

            return RedirectToAction("Index");
        }

        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            if (id <= 0)
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

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _Employees.Delete(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
