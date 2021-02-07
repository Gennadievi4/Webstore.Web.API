using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Controllers;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class BlogsControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            var blogController = new BlogsController();
            var result = blogController.Index();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var blogController = new BlogsController();
            var result = blogController.BlogSingle();
            Assert.IsType<ViewResult>(result);
        }
    }
}
