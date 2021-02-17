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
    public class ProductDetailsController : Controller
    {

        private const string __PageSize = "CatalogPageSize";

        private readonly IProductData _ProductData;
        private readonly IConfiguration _Configuration;

        public ProductDetailsController(IProductData Product, IConfiguration Configuration)
        {
            _ProductData = Product;
            _Configuration = Configuration;
        }

        public IActionResult ProductDetailsIndex(int Id)
        {
            var product = _ProductData.GetProductById(Id);

            if (product is null)
                return NotFound();

            return View(product.FromDTO().ToView());
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
