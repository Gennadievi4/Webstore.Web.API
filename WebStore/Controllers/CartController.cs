﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

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
    }
}
