using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using E_Commerce_Domain.Contracts;
using E_Commerce_Domain.Entities;
using E_Commerce_Domain.Entities.Products;
using E_Commerce_Infrastructure.Date;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace E_Commerce_Infrastructure.Date.DataSeeding
{
    public class CatalogDataSeeder(StoreDbContext _context , ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct)
        {
            try
            {
                var pendingMigrations = await  _context.Database.GetPendingMigrationsAsync(ct);
                if (pendingMigrations.Any())
                {
                    await _context.Database.MigrateAsync();
                }

                var seedRoot = Path.Combine(AppContext.BaseDirectory, "Dataseed");

                await SeedIfEmptyAsync<ProductBrand, int>(seedRoot, "brands.json",ct);
                await SeedIfEmptyAsync<ProductType, int>(seedRoot, "types.json", ct);
                await SeedIfEmptyAsync<Product, int>(seedRoot, "products.json", ct);

                int result = await _context.SaveChangesAsync(ct);
                if (result > 0)
                    logger.LogInformation($"{result} rows added");
                else
                    logger.LogInformation($"Database have been already seeded");
            }
            catch
            {

            }
        }

        public async Task SeedIfEmptyAsync<T, TKey>(string rootPath,string fileName,CancellationToken ct = default) where T : BaseEntity<TKey>
        {
            if(await _context.Set<T>().AnyAsync(ct))
            {
                logger.LogInformation($"Table {typeof(T).Name} already has data");
                return;
            }

            var filepath = Path.Combine(rootPath, fileName);
            if(!File.Exists(filepath))
            {
                logger.LogInformation($" Path :{fileName} not exist");
                return;
            }

            using var fileStream = File.OpenRead(filepath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream,options,ct);

            if(items?.Any() ?? false)
                _context.Set<T>().AddRange(items);
        }
    }
}
