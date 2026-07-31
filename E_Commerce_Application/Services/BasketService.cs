using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Application.Common;
using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos.Baskets;
using E_Commerce_Domain.Contracts;
using E_Commerce_Domain.Entities.Basket;

namespace E_Commerce_Application.Services
{
    public class BasketService(IMapper mapper , IBasketRepository basketRepository) : IBasketService
    {
        public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, CancellationToken ct = default)
        {
            var customerBasket = mapper.Map<CustomerBasket>(basket);
            var result = await  basketRepository.CreateOrUpdateBasketAsync(customerBasket);
            return result != null ? Result<BasketDto>.OK(mapper.Map<BasketDto>(customerBasket)) : Result<BasketDto>.
                Fail(Error.Failure("BasketCreateOrUpdate.Failure","Failed to Create or Update Basket"));
        }

        public async Task<Result<bool>> DeleteBasketAsync(string id, CancellationToken ct = default)
        {
            var result = await basketRepository.DeleteBasketAsync(id);
            return result ? Result<bool>.OK(true) : Result<bool>.
                Fail(Error.Failure("BasketDelete.Failure", "Failed to Delete Basket"));
        }

        public async Task<Result<BasketDto>> GetBasketAsync(string id, CancellationToken ct = default)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            return basket != null ? Result<BasketDto>.OK(mapper.Map<BasketDto>(basket)) : Result<BasketDto>.
                Fail(Error.NotFound("BasketNotFound", "Basket Not Found"));
        }
    }
}
