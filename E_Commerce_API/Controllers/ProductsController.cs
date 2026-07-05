using E_Commerce_Application.Common;
using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    public class ProductsController(IProductService _productService) : ApiBaseController
    {
        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts(int? brandId , int? typeId ,CancellationToken ct)
        {
            var result = await _productService.GetAllProductsAsync(brandId, typeId, ct);
            return ToActionResult(result);
        }

        // GET: api/Product/id

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id, CancellationToken ct)
        {
            var result = await _productService.GetProductByIdAsync(id, ct);
            return ToActionResult(result);
        }

        // GET : api/Product/brands

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct)
        {
            var result = await _productService.GetAllBrandsAsync(ct);
            return ToActionResult(result);
        }

        // GEt : api/Products/types

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct)
        {
            var result = await _productService.GetAllTypesAsync(ct);
            return ToActionResult(result);
        }



    }
}
