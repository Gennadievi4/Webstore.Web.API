﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entitys.Base;
using WebStore.Domain.Identity;

namespace WebStore.Domain.Entitys.Orders
{
    public class Order : NamedEntity
    {
        [Required]
        public User User { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Adress { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
