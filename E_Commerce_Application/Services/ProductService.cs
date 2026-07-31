using AutoMapper;
using E_Commerce_Application.Common;
using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos.Products;
using E_Commerce_Application.Params;
using E_Commerce_Application.Services.Specifications;
using E_Commerce_Domain.Contracts;
using E_Commerce_Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Application.Services
{
    public class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct)
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync(ct);
            var data = _mapper.Map<IReadOnlyList<BrandDto>>(brands);
            return Result<IReadOnlyList<BrandDto>>.OK(data);
        }

        public async Task<Result<PaginatedResult<ProductDto>>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken ct)
        {
            var spec = new ProductSpecification(queryParams);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(spec,ct);
            var mappedData = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            var countSpec = new ProductCountSpecification(queryParams); 
            var totalCount = await _unitOfWork.GetRepository<Product,int>().GetProductsCountWithSpecAsync(countSpec,ct);
            return Result<PaginatedResult<ProductDto>>.OK(new PaginatedResult<ProductDto>
                (mappedData,queryParams.PageIndex,products.Count,totalCount));
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct)
        {
            return Result<IReadOnlyList<TypeDto>>.OK(_mapper.Map<IReadOnlyList<TypeDto>>(await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(ct)));
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync( int id, CancellationToken ct)
        {
            var spec = new ProductSpecification(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdWithSpecAsync(spec, ct);
            if (product is null)
            {
                return Result<ProductDto>.Fail(Error.NotFound("Product.NotFound",$"Product with id {id} not found"));
            }
            var data = _mapper.Map<ProductDto>(product);
            return Result<ProductDto>.OK(data);
        }
    }
}
