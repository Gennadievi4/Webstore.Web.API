using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult ContactUsIndex()
        {
            return View();
        }
    }
}
