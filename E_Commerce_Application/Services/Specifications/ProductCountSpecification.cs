using E_Commerce_Application.Params;
using E_Commerce_Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Application.Services.Specifications
{
    public class ProductCountSpecification : BaseSpecification<Product,int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams) : base(p => (!queryParams.brandID.HasValue || p.BrandId == queryParams.brandID)
                                                                      && (!queryParams.typeID.HasValue || p.TypeId == queryParams.typeID)
                                                                      && (string.IsNullOrEmpty(queryParams.searchValue)
                                                                      || p.Name.ToLower().Contains(queryParams.searchValue.ToLower())))
        {
            
        }
    }
}
