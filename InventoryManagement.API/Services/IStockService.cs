namespace InventoryManagement.API.Services
{
    public interface IStockService
    {
        Task<int?> GetStockAsync(int productId);
        Task<bool> AddStockAsync(int productId, int quantity);
        Task<bool> ReduceStockAsync(int productId, int quantity);
    }
}