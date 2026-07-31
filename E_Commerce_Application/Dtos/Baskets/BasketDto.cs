using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Entities.Basket;

namespace E_Commerce_Application.Dtos.Baskets
{
    public class BasketDto
    {
        public string Id { get; set; } = string.Empty;
        public List<BasketItem> Items { get; set; } = [];
    }
}
