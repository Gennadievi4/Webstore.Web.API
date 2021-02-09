using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using System.Collections.Generic;
using System.Linq;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers.API
{
    public class SiteMap : Controller
    {
        public IActionResult Index([FromServices] IProductData ProductData)
        {
            var nodes = new List<SitemapNode>
            {
                new SitemapNode(Url.Action("Index", "Home")),
                new SitemapNode(Url.Action("ContactUsIndex", "ContactUs")),
                new SitemapNode(Url.Action("Index", "Blogs")),
                new SitemapNode(Url.Action("BlogSingle", "Blogs")),
                new SitemapNode(Url.Action("ShopIndex", "Shop")),
                new SitemapNode(Url.Action("Index", "WebApi")),
            };

            nodes.AddRange(ProductData.GetSections().Select(x => new SitemapNode(Url.Action("ShopIndex", "Shop", new { SectionId = x.Id }))));

            foreach (var item in ProductData.GetBrands())
            {
                nodes.Add(new SitemapNode(Url.Action("ShopIndex", "Shop", new { BrandId = item.id })));
            }

            foreach (var item in ProductData.GetProducts())
            {
                nodes.Add(new SitemapNode(Url.Action("Shopindex", "Shop", new { item.Id })));
            }

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}
