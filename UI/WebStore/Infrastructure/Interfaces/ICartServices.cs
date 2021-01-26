﻿using WebStore.ViewModels;

namespace WebStore.Infrastructure.Interfaces
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
