using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration Configuration) 
            : base(Configuration, WebApi.Products)
        {

        }

        public BrandDTO GetBrandById(int Id) => Get<BrandDTO>($"{Address}/brands/{Id}");

        public IEnumerable<BrandDTO> GetBrands() => Get<IEnumerable<BrandDTO>>($"{Address}/brands");

        public ProductDTO GetProductById(int Id) => Get<ProductDTO>($"{Address}/{Id}");

        public PageProductsDTO GetProducts(ProductFilter Filter = null)
        {
            var response = Post(Address, Filter ?? new ProductFilter());
            var result = response.Content
                .ReadAsAsync<PageProductsDTO>()
                .Result;
            return result;
        }

        public SectionDTO GetSectionById(int Id) => Get<SectionDTO>($"{Address}/sections/{Id}");

        public IEnumerable<SectionDTO> GetSections() => Get<IEnumerable<SectionDTO>>($"{Address}/sections");
    }
}
