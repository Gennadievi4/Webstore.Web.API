﻿using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain;
using WebStore.Domain.Entitys;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services.InMemory;

namespace WebStore.Infrastructure.Services
{
    [Obsolete("Класс устарел, так как с самого начала использовался в учебных целях. Используйте класс SqlProductData", true)]
    public class InMemoryProductData : IProductData
    {
        private readonly DbInMemory _Db;
        public InMemoryProductData(IEmployeesData db) => _Db = (DbInMemory)db;
        public IEnumerable<Brand> GetBrands() => _Db.Brands;
        public IEnumerable<Section> GetSections() => _Db.Sections;
        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            var query = _Db.Products;

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
