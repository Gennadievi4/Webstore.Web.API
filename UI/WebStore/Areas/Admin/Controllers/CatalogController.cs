using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entitys;
using WebStore.Domain.Identity;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrator)]
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;

        public CatalogController(IProductData ProductData) => this._ProductData = ProductData;
        public IActionResult Index()
        {
            return View(_ProductData.GetProducts().Products.FromDTO());
        }

        public IActionResult Edit(int Id)
        {
            var product = _ProductData.GetProductById(Id);
            if (product is null) return NotFound();
            return View(product);
        }

        //public IActionResult Edit(Product Product)
        //{
        //    if (!ModelState.IsValid) return View(Product);

        //    var product = new Product
        //    {
        //        Name = Product.Name,
        //        Brand = Product.Brand,
        //        Section = Product.Section,
        //        ImageUrl = Product.ImageUrl,
        //        Price = Product.Price
        //    };

        //    product.
        //}

        public IActionResult Delete(int Id)
        {
            var product = _ProductData.GetProductById(Id);
            if (product is null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(Product product)
        {
            //Не забыть написать логику удаления

            return RedirectToAction(nameof(Index));
        }
    }       
}
