using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Controllers;
using WebStore.Interfaces.Services;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class CartControllerTests
    {
        public async Task CheckOut_ModelState_Invalid_View_with_Model()
        {
            var cart_service_mock = new Mock<ICartServices>();

            var controller = new CartController(cart_service_mock.Object);
        }
    }
}
