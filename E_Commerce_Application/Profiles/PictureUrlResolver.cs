using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Application.Dtos;
using E_Commerce_Domain.Entities.Products;
using Microsoft.Extensions.Options;

namespace E_Commerce_Application.Profiles
{
    public class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly UrlSettings _urlSettings;
        public PictureUrlResolver(IOptions<UrlSettings> options)
        {
            _urlSettings = options.Value;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            var path = source.PictureUrl.TrimStart('/');   
            var baseUrl = _urlSettings.BaseUrl.TrimEnd('/');
            return $"{baseUrl}/Files/{path}";

        }
    }

    public class UrlSettings
    {
        public string BaseUrl { get; set; }
    }
}
