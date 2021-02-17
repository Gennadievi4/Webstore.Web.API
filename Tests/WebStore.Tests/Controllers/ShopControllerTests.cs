using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using WebStore.Controllers;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class ShopControllerTests
    {
        [TestMethod]
        public void Shop_Returns_CorrectView()
        {
            var products = new[]
            {
                new ProductDTO(1, "Product1", 1, 10m, "Iamge1.png", new BrandDTO(1, "Brand1", 1, 1), new SectionDTO(1, "Section1", 1, null, 1)),
                new ProductDTO(2, "Product2", 2, 20m, "Iamge2.png", new BrandDTO(2, "Brand2", 1, 1), new SectionDTO(2, "Section2", 2, null, 1)),
                new ProductDTO(3, "Product3", 3, 30m, "Iamge3.png", new BrandDTO(3, "Brand3", 1, 1), new SectionDTO(3, "Section3", 3, null, 1)),
            };

            const int expected_section_id = 1;
            const int expected_brand_id = 3;

            var product_data_mock = new Mock<IProductData>();
            product_data_mock
                .Setup(x => x.GetProducts(It.IsAny<ProductFilter>()))
                .Returns(new PageProductsDTO(products, products.Length));

            var configuration_mock = new Mock<IConfiguration>();
            configuration_mock.Setup(configuration => configuration[It.IsAny<string>()])
                .Returns("3");

            var controller = new ShopController(product_data_mock.Object, configuration_mock.Object);

            var result = controller.ShopIndex(expected_brand_id, expected_section_id);

            var view_result = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<CatalogViewModel>(view_result.ViewData.Model);


            Assert.Equal(products.Length, model.Product.Count());
            Assert.Equal(expected_brand_id, model.BrandId);
            Assert.Equal(expected_section_id, model.SectionId);
        }
    }
}
