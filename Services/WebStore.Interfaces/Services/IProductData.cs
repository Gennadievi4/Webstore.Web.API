using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    {
        SectionDTO GetSectionById(int Id);
        IEnumerable<SectionDTO> GetSections();
        IEnumerable<BrandDTO> GetBrands();
        PageProductsDTO GetProducts(ProductFilter Filter = null);
        ProductDTO GetProductById(int Id);
        BrandDTO GetBrandById(int Id);
    }
}