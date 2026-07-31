using E_Commerce_Application.Contracts;
using E_Commerce_Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Application
{
    public static class ApplicationServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(c => { }, typeof(ApplicationServiceRegister).Assembly);
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();

            return services;
        }
    }
}
