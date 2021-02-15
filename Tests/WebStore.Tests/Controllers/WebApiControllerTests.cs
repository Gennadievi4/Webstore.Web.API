using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebStore.Controllers;
using WebStore.Interfaces.TestApi;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class WebApiControllerTests
    {
        private readonly IValuesService _ValueService;

        [TestMethod]
        public void Index_Returns_View_with_Values()
        {
            var expected_values = new[] { "1", "2", "3" };

            var values_service_mock = new Mock<IValuesService>();// 
            values_service_mock                                  //
                .Setup(x => x.Get())                             // Стаб - некоторый результат, который будет использован внутри тестового объекта.
                .Returns(expected_values);                       //(прикидываемся интерфейсом)

            var controller = new WebApiController(values_service_mock.Object);// Stab

            var result = controller.Index();

            var view_result = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<string>>(view_result.Model);

            Assert.Equal(expected_values.Length, model.Count());

            //Mock

            values_service_mock.Verify(service => service.Get());
            values_service_mock.VerifyNoOtherCalls();
        }
    }
}
