using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<Employee> _Db;
        public HomeController(DbInMemory db)
        {
            _Db = db._Employees;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employees()
        {
            return View(_Db);
        }

        public IActionResult Details(int Id)
        {
            var employees = _Db.Where(x => x.Id == Id).Single();
            return View(employees);
        }
    }
}
