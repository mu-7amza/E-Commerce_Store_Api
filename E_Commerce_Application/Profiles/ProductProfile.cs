using AutoMapper;
using E_Commerce_Application.Dtos.Products;
using E_Commerce_Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand,BrandDto>();
            CreateMap<ProductType, TypeDto>();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductBrand,opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());


        }
    }
}
