using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _Configuration;

        public ShopController(IProductData product, IConfiguration Configuration)
        {
            _ProductData = product;
            _Configuration = Configuration;
        }

        public IActionResult ShopIndex(int? BrandId, int? SectionId, int page = 1, int? PageSize = null)
        {
            var page_size = PageSize
                ?? (int.TryParse(_Configuration["CatalogPageSize"], out var value) ? value: null);

            var filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Page = page,
                PageSize = page_size,
            };

            var products = _ProductData.GetProducts(filter);

            return View(new CatalogViewModel
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Product = products.Products
                    .OrderBy(p => p.Order)
                    .FromDTO()
                    .ToView(),
                PageViewModel = new PageViewModel
                {
                    Page = page,
                    PageSize = page_size ?? 0,
                    TotalItems = products.TotalCount,
                }
            });
        }
    }
}
