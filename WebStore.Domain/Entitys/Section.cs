using WebStore.Domain.Entitys.Base;
using WebStore.Domain.Entitys.Base.Interfaces;

namespace WebStore.Domain.Entitys
{
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public int? ParentId { get; set; }
    }
}