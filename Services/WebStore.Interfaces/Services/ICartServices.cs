using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Services
{
    public interface ICartServices
    {
        void AddToCart(int id);
        void DecrementFromCart(int id);
        void RemoveFromCart(int id);
        void Clear();
        CartViewModel TransformFromCart();
    }
}
