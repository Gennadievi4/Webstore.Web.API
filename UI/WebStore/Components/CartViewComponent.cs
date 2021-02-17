using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartServices _CartService;

        public CartViewComponent(ICartServices CartService)
        {
            _CartService = CartService;
        }

        public IViewComponentResult Invoke() => View(_CartService.TransformFromCart());
    }
}
