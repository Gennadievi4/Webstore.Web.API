﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices _CartServices;

        public CartController(ICartServices CartServices) 
        {
            _CartServices = CartServices;
        }

        [AllowAnonymous]
        public IActionResult CartIndex() => View(new CartOrderViewModel 
        { 
            Cart = _CartServices.TransformFromCart()
        });

        [AllowAnonymous]
        public IActionResult AddToCart(int Id)
        {
            _CartServices.AddToCart(Id);
            return RedirectToAction(nameof(CartIndex));
        }

        public IActionResult RemoveFromCart(int id)
        {
            _CartServices.RemoveFromCart(id);
            return RedirectToAction(nameof(CartIndex));
        }

        public IActionResult DecrementFromCart(int id)
        {
            _CartServices.DecrementFromCart(id);
            return RedirectToAction(nameof(CartIndex));
        }

        public IActionResult Clear()
        {
            _CartServices.Clear();
            return RedirectToAction(nameof(CartIndex));
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> CheckOut(OrderViewModel OrderModel, [FromServices] IOrderService OrderServices)
        {
            if (!ModelState.IsValid)
                return View(nameof(CartIndex), new CartOrderViewModel
                {
                    Cart = _CartServices.TransformFromCart(),
                    Order = OrderModel
                });

            var create_order_model = new CreateOrderModel(
                OrderModel, 
                _CartServices.TransformFromCart().Items
                    .Select(x => new OrderItemDTO(
                        x.Product.Id, 
                        x.Product.Price, 
                        x.Quatity))
                    .ToList());

            var order = await OrderServices.CreateOrder(User.Identity!.Name, create_order_model);
            //var order = await OrderServices.CreateOrder(
            //    User.Identity!.Name,
            //    _CartServices.TransformFromCart(),
            //    OrderModel);

            _CartServices.Clear();

            return RedirectToAction("OrderConfirmed", new { order.Id });
        }
    }
}
