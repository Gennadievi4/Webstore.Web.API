using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult ShopIndex()
        {
            return View();
        }
    }
}
