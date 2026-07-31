using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Params;
using E_Commerce_Domain.Entities.Products;

namespace E_Commerce_Application.Services.Specifications
{
    public class ProductSpecification : BaseSpecification<Product,int>
    {
        public ProductSpecification(ProductQueryParams queryParams) : base(p => (!queryParams.brandID.HasValue || p.BrandId == queryParams.brandID)
                                                                      && (!queryParams.typeID.HasValue || p.TypeId == queryParams.typeID) 
                                                                      && (string.IsNullOrEmpty(queryParams.searchValue)
                                                                      || p.Name.ToLower().Contains(queryParams.searchValue.ToLower()))) 
                                                                      
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    OrderByAsc(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    OrderByDescendin(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    OrderByAsc(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    OrderByDescendin(p => p.Price);
                    break;
                default:
                    OrderByAsc(p => p.Name);
                    break;
            }

            ApplyPagination(queryParams.Pagesize,queryParams.PageIndex);
        }

        
        public ProductSpecification(int id) : base(P => P.Id == id )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
