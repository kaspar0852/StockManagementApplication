using api.EntityFrameworkCore;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Stocks;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<StockRepository> _logger;


    public StockRepository(ApplicationDbContext dbContext, 
        ILogger<StockRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Stock?> GetStockById(int id)
    {
        _logger.LogInformation("GetStockById requested");
        
        var requiredStockData = await _dbContext.Stock
            .Include(c => c.Comments)
            .FirstOrDefaultAsync(s => s.Id == id);
        
        return requiredStockData;
    }

    public async Task CreateStock(Stock stock)
    {
        _logger.LogInformation("CreateStock requested");
        
        await _dbContext.Stock.AddAsync(stock);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateStock(Stock stock)
    {
        _logger.LogInformation("UpdateStock requested");
        
        _dbContext.Stock.Update(stock);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteStock(int id)
    {
        _logger.LogInformation("DeleteStock requested");
        
        var stock = await _dbContext.Stock.FindAsync(id);
        if (stock!= null)
        {
            _dbContext.Stock.Remove(stock);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("Stock not found");
        }
    }


    public async Task<List<Stock>> GetAllStocks()
    {
        _logger.LogInformation("GetAllStocks requested");
        
        var stocks = await _dbContext.Stock.ToListAsync();
        
        return stocks;
    }

    public async Task<bool> StockExists(int id)
    {
        return await _dbContext.Stock.AnyAsync(e => e.Id == id);
    }
}