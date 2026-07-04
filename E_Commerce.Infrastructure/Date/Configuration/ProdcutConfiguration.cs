using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce_Infrastructure.Date.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.ProductBrand).WithMany().HasForeignKey(P => P.BrandId);
            builder.HasOne(P => P.ProductType).WithMany().HasForeignKey(P => P.TypeId);

            builder.Property(P => P.Price).HasColumnType("decimal(10,2)");
            builder.Property(P => P.Description).HasMaxLength(500);
            builder.Property(P => P.Name).HasMaxLength(200);
            builder.Property(P => P.PictureUrl).HasMaxLength(100);



        }
    }
}
