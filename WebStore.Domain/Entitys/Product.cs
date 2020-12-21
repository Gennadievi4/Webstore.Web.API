using WebStore.Domain.Entitys.Base;
using WebStore.Domain.Entitys.Base.Interfaces;

namespace WebStore.Domain.Entitys
{
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }

        public int BrandId { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}
