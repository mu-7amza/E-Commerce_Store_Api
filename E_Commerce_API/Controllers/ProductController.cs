using E_Commerce_Application.Common;
using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService _productService) : Controller
    {
        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<Result<IReadOnlyList<ProductDto>>>> GetAllProducts(CancellationToken ct)
        {
            var result = await _productService.GetAllProductsAsync(ct);
            return result;
        }

        // GET: api/Product/id

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<ProductDto>>> GetProductById(int id, CancellationToken ct)
        {
            var result = await _productService.GetProductByIdAsync(id, ct);
            return result;
        }

        // GET : api/Product/brands

        [HttpGet("brands")]
        public async Task<ActionResult<Result<IReadOnlyList<BrandDto>>>> GetAllBrands(CancellationToken ct)
        {
            var result = await _productService.GetAllBrandsAsync(ct);
            return result;
        }

        // GEt : api/Products/types

        [HttpGet("types")]
        public async Task<ActionResult<Result<IReadOnlyList<TypeDto>>>> GetAllTypes(CancellationToken ct)
        {
            var result = await _productService.GetAllTypesAsync(ct);
            return result;
        }

    }
}
