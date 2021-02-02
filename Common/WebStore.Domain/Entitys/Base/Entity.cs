using WebStore.Domain.Entitys.Base.Interfaces;

namespace WebStore.Domain.Entitys.Base
{
    /// <summary>Сущность</summary>
    public abstract class Entity : IEntity
    {
        /// <summary>Идентификатор</summary>
        public int Id { get; set; }
    }
}
