using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductData _ProductData;

        public ShopController(IProductData product) => _ProductData = product;

        public IActionResult ShopIndex(int? BrandId, int? SectionId)
        {
            var filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId,
            };

            var products = _ProductData.GetProducts(filter);

            return View(new CatalogViewModel
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Product = products
                    .OrderBy(p => p.Order)
                    .ToView()
            });
        }
    }
}
