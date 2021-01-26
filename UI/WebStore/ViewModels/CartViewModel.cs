using System.Collections.Generic;
using System.Linq;

namespace WebStore.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel Product, int Quatity)> Items { get; set; }
        public int ItemsCount => Items?.Sum(x => x.Quatity) ?? 0;

        public decimal TotalPrice => Items?.Sum(x => x.Product.Price * x.Quatity) ?? 0m;
    }
}
