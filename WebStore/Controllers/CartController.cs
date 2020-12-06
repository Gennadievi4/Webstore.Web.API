using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        public IActionResult CartIndex()
        {
            return View();
        }
    }
}
