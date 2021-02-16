using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entitys;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products.InSQL
{
    public class SqlProductData : IProductData
    {

        private readonly WebStoreDB _db;
        public SqlProductData(WebStoreDB dB) => _db = dB;

        public BrandDTO GetBrandById(int Id) => _db.Brands
            .Include(x => x.Products)
            .FirstOrDefault(x => x.Id == Id)
            .ToDTO();

        public IEnumerable<BrandDTO> GetBrands() => _db.Brands
            .Include(x => x.Products)
            .AsEnumerable()
            .ToDTO();

        public ProductDTO GetProductById(int Id) => _db.Products
            .Include(x => x.Brand)
            .Include(x => x.Section)
            .FirstOrDefault(x => x.Id == Id)
            .ToDTO();

        public PageProductsDTO GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products
                .Include(p => p.Brand)
                .Include(p => p.Section);

            if (Filter?.Ids?.Length > 0)
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

            var total_count = query.Count();

            if (Filter?.PageSize > 0)
                query = query
                    .Skip((Filter.Page - 1) * (int)Filter.PageSize)
                    .Take((int)Filter.PageSize);

            return new PageProductsDTO(query.AsEnumerable().ToDTO(), total_count);
        }

        public SectionDTO GetSectionById(int Id) => _db.Sections
            .Include(section => section.Products)
            .FirstOrDefault(x => x.Id == Id)
            .ToDTO();

        public IEnumerable<SectionDTO> GetSections() => _db.Sections
            .Include(x => x.Products)
            .AsEnumerable()
            .ToDTO();
    }
}
