using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {

        private readonly List<Employee> _Db;
        public EmployeesController(DbInMemory db) => _Db = db._Employees;
        public IActionResult Index() => View(_Db);
        public IActionResult Details(int id)
        {
            var employee = _Db.FirstOrDefault(x => x.Id == id);
            if (employee is not null)
                return View(employee);
            return NotFound();
        }
    }
}
