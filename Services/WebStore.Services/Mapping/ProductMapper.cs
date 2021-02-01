using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entitys;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product p) => new ProductViewModel()
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Brand = p.Brand?.Name,
            ImageUrl = p.ImageUrl
        };

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> p) => p.Select(ToView);
    }
}
