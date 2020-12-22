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
        public IEnumerable<Brand> GetBrands() => _db.Brands.Include(x => x.Products);

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products;

            if (Filter?.BrandId is not null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            if (Filter?.SectionId is not null)
                query = query.Where(x => x.SectionId == Filter.SectionId);

            return query;
        } 

        public IEnumerable<Section> GetSections() => _db.Sections.Include(x => x.Products);
    }
}
