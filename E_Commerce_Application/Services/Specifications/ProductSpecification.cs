using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Entities.Products;

namespace E_Commerce_Application.Services.Specifications
{
    public class ProductSpecification : BaseSpecification<Product,int>
    {
        public ProductSpecification(int? brandId ,int? typeId) : base(p => (!brandId.HasValue || p.BrandId == brandId)
                                                                      && (!typeId.HasValue || p.TypeId == typeId))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        public ProductSpecification(int id) : base(P => P.Id == id )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
