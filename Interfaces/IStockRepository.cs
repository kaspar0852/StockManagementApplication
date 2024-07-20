using api.Models;

namespace api.Interfaces;

public interface IStockRepository
{
    Task<Stock?> GetStockById(int id);
    Task<List<Stock>> GetAllStocks();

    Task DeleteStock(int id);

    Task CreateStock(Stock stock);

    Task<bool> StockExists(int id);

    Task UpdateStock(Stock stock);
}