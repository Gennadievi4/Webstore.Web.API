namespace WebStore.Domain.Entitys.Base.Interfaces
{
    public interface IOerderedEntity : IEntity
    {
        int Order { get; set; }
    }
}
