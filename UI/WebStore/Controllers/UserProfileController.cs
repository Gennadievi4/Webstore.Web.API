using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Order([FromServices] IOrderService OrderService)
        {
            var orders = await OrderService.GetUserOrders(User.Identity.Name);

            return View(orders.Select(order => new UserOrderViewModel(
            order.Id,
            order.Name,
            order.Phone,
            order.Adress,
            order.Items.Sum(item => item.Price * item.Quantity)
            )));
        }
    }
}
