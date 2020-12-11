using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.Entitys;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();

        IEnumerable<Product> GetProducts(ProductFilter Filter = null);
    }
}
