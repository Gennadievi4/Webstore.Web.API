﻿using System;
using System.Collections.Generic;
using WebStore.Domain.ViewModels;

namespace WebStore.Domain.DTO.Orders
{
    public record OrderItemDTO(int Id, decimal Price, int Quantity);

    public record OrderDTO(
        int Id, 
        string name, 
        string Phone, 
        string Adress, 
        DateTime Date, 
        IEnumerable<OrderItemDTO> Items
        );

    public record CreateOrderModel(OrderViewModel Order, IList<OrderItemDTO> Items);
}
