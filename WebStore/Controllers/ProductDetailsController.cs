using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;

namespace WebStore.Controllers
{
    public class ProductDetailsController : Controller
    {

        private readonly IProductData _ProductData;

        public ProductDetailsController(IProductData product)
        {
            _ProductData = product;
        }

        public IActionResult ProductDetailsIndex(int Id)
        {
            var product = _ProductData.GetProductById(Id);

            if (product is null)
                return NotFound();

            return View(product.ToView());
        }
    }
}
