using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    public class ShopController : Controller
    {
        private const string __PageSize = "CatalogPageSize";

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
                ?? (int.TryParse(_Configuration[__PageSize], out var value) ? value: null);

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

        #region WebAPI

        public IActionResult GetFeaturesItems(int? BrandId, int? SectionId, int page = 1, int? PageSize = null)
        {
            return PartialView("Partial/_FeaturesItems", GetProducts(BrandId, SectionId, page, PageSize));
        }

        private IEnumerable<ProductViewModel> GetProducts(int? BrandId, int? SectionId, int Page, int? PageSize)
        {
            return _ProductData.GetProducts(new ProductFilter
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Page = Page,
                PageSize = PageSize
                     ?? (int.TryParse(_Configuration[__PageSize], out var size) ? size : null)
            })
             .Products.OrderBy(x => x.Order)
             .FromDTO()
             .ToView();
        }

        #endregion
    }
}
