using InventoryManagement.API.Data;
using InventoryManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            // Reset IDENTITY seed to the max ID in the table
            await ResetIdentitySeedAsync();
        }

        private async Task ResetIdentitySeedAsync()
        {
            try
            {
                // Get the max ID from the table
                var maxId = await _context.Products.MaxAsync(p => (int?)p.Id) ?? 0;

                // Reset IDENTITY seed to maxId so next insert gets maxId + 1
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"DBCC CHECKIDENT ('Products', RESEED, {maxId})";
                    await command.ExecuteNonQueryAsync();
                }

                await connection.CloseAsync();
            }
            catch (Exception ex)
            {
                // Log the error but don't throw - deletion already succeeded
                Console.WriteLine($"Failed to reset IDENTITY seed: {ex.Message}");
            }
        }
    }
}