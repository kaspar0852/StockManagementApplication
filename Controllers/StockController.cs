using api.Dtos;
using api.Dtos.Stocks;
using api.EntityFrameworkCore;
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

    public StockController(ApplicationDbContext context, ILogger<StockController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ResponseDto<List<StockDto>>> GetAllStockAsync()
    {
        try
        {
            _logger.LogInformation("GetAllStockAsync requested");
            
            var stocks = await _context.Stock
                .Select(x => x.ToStockDto())
                .ToListAsync();
            
            _logger.LogInformation("GetAllStockAsync responded");

            return new ResponseDto<List<StockDto>>
            {
                Success = true,
                Code = 200,
                Message = "Success",
                Data = stocks
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<ResponseDto<Stock>> GetStockByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("GetStockByIdAsync requested");
            
            var requestedStock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (requestedStock is null)
                return new ResponseDto<Stock>
                {
                    Success = false,
                    Code = 400,
                    Message = $"Stock with id: {id} not found.",
                    Data = null
                };

            _logger.LogInformation("GetStockByIdAsync responded");
            return new ResponseDto<Stock>
            {
                Success = true,
                Code = 400,
                Message = "Success",
                Data = requestedStock
            };

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    
    
}