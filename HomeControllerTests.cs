using System;
using Microsoft.VisualBasic.TestTools.UnitTesting;

public class HomeControllerTests
{
	[TestClass]
	public HomeControllerTests()
	{
		[TestMethod]
		public void Index_Returns_View()
        {
			//A-A-A = Arrange - Act - Assert = Подготовка данных - выполнение действия - проверка результатов
			var controller = new HomeController();

			var result = controller.Index();
        }
	}
}
