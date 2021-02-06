using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        }
    }
}
