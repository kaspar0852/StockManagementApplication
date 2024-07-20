using api.Dtos;
using api.Dtos.Stocks;
using api.EntityFrameworkCore;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<StockController> _logger;
    private readonly IStockRepository _stockRepository;

    public StockController(ApplicationDbContext context, ILogger<StockController> logger, IStockRepository stockRepository)
    {
        _context = context;
        _logger = logger;
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<ResponseDto<List<StockDto>>> GetAllStockAsync()
    {
        try
        {
            _logger.LogInformation("GetAllStockAsync requested");
            
            var stocks = await _context.Stock
                .Include(c => c.Comments)
                .Select(x => x.ToStockDto())
                .ToListAsync();
            
            _logger.LogInformation("GetAllStockAsync responded");
            
            return this.SendSuccess(data: stocks, message:"Success");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<ResponseDto<StockDto>> GetStockByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("GetStockByIdAsync requested");
            
            var requestedStock = await _stockRepository.GetStockById(id);

            if (requestedStock is null)
            {
                return this.SendFailure<StockDto>(null,"Stock not found");
            }
            
            var result = requestedStock.ToStockDto();

            _logger.LogInformation("GetStockByIdAsync responded");
            return this.SendSuccess(result,"Stock found");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<ResponseDto<string>> PostStockDataAsync(CreateStockDto input)
    {
        try
        {
            _logger.LogInformation("PostStockDataAsync requested");

            var stock = input.CreateStockRequestDto();

            await _stockRepository.CreateStock(stock);
            
            _logger.LogInformation("PostStockDataAsync responded");
            
            return this.SendSuccess(data: string.Empty, message: "Stock added successfully");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ResponseDto<StockDto>> UpdateStockDataAsync(int id, UpdateStockDto input)
    {
        try
        {
            _logger.LogInformation("UpdateStockDataAsync requested");
            
            var requestedStock = await _stockRepository.GetStockById(id);
            
            if (requestedStock is null)
            {
                return this.SendFailure<StockDto>(null,"Stock not found");
            }
            
            requestedStock.Symbol = input.Symbol;
            requestedStock.CompanyName = input.CompanyName;
            requestedStock.Purchase = input.Purchase;
            requestedStock.LastDiv = input.LastDiv;
            requestedStock.Industry = input.Industry;
            requestedStock.MarketCap = input.MarketCap;
            
            await _stockRepository.UpdateStock(requestedStock);
            
            var result = requestedStock.ToStockDto();
            
            _logger.LogInformation("UpdateStockDataAsync responded");
            return this.SendSuccess(result, "Stock updated successfully");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ResponseDto<string>> DeleteStockDataAsync(int id)
    {
        try
        {
            _logger.LogInformation("DeleteStockDataAsync requested");
            
            await _stockRepository.DeleteStock(id);
            
            _logger.LogInformation("DeleteStockDataAsync responded");
            
            return this.SendSuccess(data: string.Empty, message: "Stock deleted successfully");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }



}