using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain;
using WebStore.Domain.Entitys;

namespace WebStore.Services.Products.InMemory
{
    [Obsolete("Класс устарел, так как с самого начала использовался в учебных целях. Используйте класс SqlProductData", true)]
    public class InMemoryProductData
    {
        public InMemoryProductData() { }
        public IEnumerable<Brand> GetBrands() => DbInMemory.Brands;
        public IEnumerable<Section> GetSections() => DbInMemory.Sections;
        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            var query = DbInMemory.Products;

            if (Filter?.SectionId is { } section_id)
                query = query.Where(product => product.SectionId == section_id);

            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            return query;
        }

        public Section GetSectionById(int Id) => throw new NotSupportedException();

        public Product GetProductById(int Id) => throw new NotSupportedException();

        public Brand GetBrandById(int Id) => throw new NotSupportedException();
    }
}
