using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class CheckOutController : Controller
    {
        public IActionResult CheckOutIndex()
        {
            return View();
        }
    }
}
