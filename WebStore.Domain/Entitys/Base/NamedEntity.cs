using WebStore.Domain.Entitys.Base.Interfaces;

namespace WebStore.Domain.Entitys.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; set; }
    }
}
