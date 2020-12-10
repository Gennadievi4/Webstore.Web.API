using WebStore.Domain.Entitys.Base;
using WebStore.Domain.Entitys.Base.Interfaces;

namespace WebStore.Domain.Entitys
{
    public class Brand : NamedEntity, IOerderedEntity
    {
        public int Order { get; set; }
    }
}