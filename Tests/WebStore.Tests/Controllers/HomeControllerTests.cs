using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebStore.Controllers;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            //A-A-A = Arrange - Act - Assert = Подготовка данных - выполнение действия - проверка результатов
            var controller = new HomeController();

            var result = controller.Index();

            //Assert.IsInstanceOfType(result, typeof(ViewResult)); - MSTest

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Throw_throw_ApplicationException()
        {
            var controller = new HomeController();
            var exception_message = "Test";
            var result = controller.Throw(exception_message);
        }

        [TestMethod]
        public void Error404_Returns_View()
        {
            var controller = new HomeController();
            var result = controller.Error404();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ErrorStatus_404_RedirectTo_Error404()
        {
            var controller = new HomeController();
            const string error_status_code = "404";
            const string expected_action_name = nameof(HomeController.Error404);

            var result = controller.ErrorStatus(error_status_code);

            //Assert.NotNull(result);

            var redirect_to_action = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(expected_action_name, redirect_to_action.ActionName);
            Assert.Null(redirect_to_action.ControllerName);
        }
    }
}
