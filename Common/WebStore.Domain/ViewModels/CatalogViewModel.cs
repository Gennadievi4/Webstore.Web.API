using System.Collections.Generic;

namespace WebStore.Domain.ViewModels
{
    public class CatalogViewModel
    {
        public IEnumerable<ProductViewModel> Product { get; set; }
        public int? SectionId { get; set; }
        public int? BrandId { get; set; }
    }
}
