using E_Commerce_Domain.Contracts;

namespace E_Commerce_API
{
    public static class AppExtensions
    {
        public static async Task<WebApplication> SeedAndMigrateData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            var identitySeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Identity");
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            await seeder.SeedDataAsync();
            await identitySeeder.SeedDataAsync();
            return app;
        }
    }
}
