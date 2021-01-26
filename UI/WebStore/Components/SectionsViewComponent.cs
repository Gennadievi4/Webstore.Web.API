using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;
        public SectionsViewComponent(IProductData ProductData) => _ProductData = ProductData;
        public IViewComponentResult Invoke()
        {
            var section = _ProductData.GetSections().ToArray();

            var parent_sections = section.Where(x => x.ParentId is null);

            var parent_section_views = parent_sections
                .Select(x => new SectionViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Order = x.Order,
                    ProductCount = x.Products.Count(),
                })
                .ToList();

            foreach (var parent_section in parent_section_views)
            {
                var childs = section.Where(x => x.ParentId == parent_section.Id);

                foreach (var child_section in childs)
                {
                    parent_section.ChildItems.Add(new SectionViewModel
                    {
                        Id = child_section.Id,
                        Name = child_section.Name,
                        Order = child_section.Order,
                        ParentSection = parent_section,
                        ProductCount = child_section.Products.Count(),
                    });
                }

                parent_section.ChildItems.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }

            parent_section_views.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));

            return View(parent_section_views);
        }
    }
}
