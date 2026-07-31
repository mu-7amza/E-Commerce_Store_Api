using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Application.Dtos.Baskets;
using E_Commerce_Domain.Entities.Basket;

namespace E_Commerce_Application.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket,BasketDto>().ReverseMap();
            CreateMap<BasketDto,CustomerBasket>().ReverseMap();

        }
    }
}
