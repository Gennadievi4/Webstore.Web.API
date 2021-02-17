using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class ProductDetailsTests
    {
        [TestMethod]
        public void Details_Returns_with_Correct_View()
        {
            #region Arrange
            const int expected_product_id = 1;
            const decimal expected_price = 10m;

            var expected_name = $"Product id {expected_product_id}";
            var expected_brand_name = $"Brand of product {expected_product_id}";

            var product_data_mock = new Mock<IProductData>();
            product_data_mock.Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns<int>(id => new ProductDTO(
                    id,
                    $"Product id {id}",
                    1,
                    expected_price,
                    $"img{id}.png",
                    new BrandDTO(1, $"Brand of product {id}", 1, 1),
                    new SectionDTO(1, $"Section of product {id}", 1, null, 1)
                    ));

            var controller = new ProductDetailsController(product_data_mock.Object);
            #endregion

            #region Act
            var result = controller.ProductDetailsIndex(expected_product_id);
            #endregion

            #region Assert

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(view_result.Model);

            Assert.Equal(expected_product_id, model.Id);
            Assert.Equal(expected_name, model.Name);
            Assert.Equal(expected_price, model.Price);
            Assert.Equal(expected_brand_name, model.Brand);

            #endregion
        }
    }
}
