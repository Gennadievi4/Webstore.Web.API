using WebStore.Domain.Entitys;

namespace WebStore.Interfaces.Services
{
    public interface ICartStore
    {
        Cart Cart { get; set; }
    }
}
