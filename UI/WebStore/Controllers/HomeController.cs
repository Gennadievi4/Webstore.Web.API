using Microsoft.AspNetCore.Mvc;
using System;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Throw(string id) => throw new ApplicationException(id);

        public IActionResult Error404()
        {
            return View();
        }

        public IActionResult ErrorStatus(string Code) => Code switch
        {
            "404" => RedirectToAction(nameof(Error404)),
            _ => Content($"Error code {Code}")
        };
    }
}
