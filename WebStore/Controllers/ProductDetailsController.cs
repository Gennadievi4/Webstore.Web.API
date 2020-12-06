using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class ProductDetailsController : Controller
    {
        public IActionResult ProductDetailsIndex()
        {
            return View();
        }
    }
}
