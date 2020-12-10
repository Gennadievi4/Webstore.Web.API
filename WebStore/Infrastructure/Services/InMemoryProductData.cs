using System.Collections.Generic;
using WebStore.Domain.Entitys;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryProductData : IProductData
    {
        private readonly DbInMemory _Db;
        public InMemoryProductData(DbInMemory db) => _Db = db;
        public IEnumerable<Brand> GetBrands() => _Db.Brands;
        public IEnumerable<Section> GetSections() => _Db.Sections;
    }
}
