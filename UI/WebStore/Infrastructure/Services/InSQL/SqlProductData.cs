using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entitys;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SqlProductData : IProductData
    {

        private readonly WebStoreDB _db;
        public SqlProductData(WebStoreDB dB) => _db = dB;

        public Brand GetBrandById(int Id) => _db.Brands
            .Include(x => x.Products)
            .FirstOrDefault(x => x.Id == Id);

        public IEnumerable<Brand> GetBrands() => _db.Brands.Include(x => x.Products);

        public Product GetProductById(int Id) => _db.Products
            .Include(x => x.Brand)
            .Include(x => x.Section)
            .FirstOrDefault(x => x.Id == Id);

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products
                .Include(p => p.Brand)
                .Include(p => p.Section);

            if(Filter?.Ids?.Length > 0)
            {
                query = query.Where(product => Filter.Ids.Contains(product.Id));
            }
            else
            {
                if (Filter?.BrandId is not null)
                    query = query.Where(product => product.BrandId == Filter.BrandId);

                if (Filter?.SectionId is not null)
                    query = query.Where(x => x.SectionId == Filter.SectionId);
            }

            return query;
        }

        public Section GetSectionById(int Id) => _db.Sections
            .Include(section => section.Products)
            .FirstOrDefault(x => x.Id == Id);

        public IEnumerable<Section> GetSections() => _db.Sections.Include(x => x.Products);
    }
}
