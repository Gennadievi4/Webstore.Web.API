using WebStore.Domain.Entitys.Base.Interfaces;

namespace WebStore.Domain.Entitys.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
