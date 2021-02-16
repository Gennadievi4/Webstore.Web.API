using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    public class ProductDetailsController : Controller
    {

        private readonly IProductData _ProductData;

        public ProductDetailsController(IProductData Product)
        {
            _ProductData = Product;
        }

        public IActionResult ProductDetailsIndex(int Id)
        {
            var product = _ProductData.GetProductById(Id);

            if (product is null)
                return NotFound();

            return View(product.FromDTO().ToView());
        }
    }
}
