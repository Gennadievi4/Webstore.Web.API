using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.Entitys;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IProductData
    {
        Section GetSectionById(int Id);
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts(ProductFilter Filter = null);
        Product GetProductById(int Id);
        Brand GetBrandById(int Id);
    }
}
