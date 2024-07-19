using api.Dtos.Stocks;
using api.Models;

namespace api.Mappers;

public static class StockMapper
{
    public static StockDto ToStockDto(this Stock model)
    {
        return new StockDto
        {
            Id = model.Id,
            Symbol = model.Symbol,
            CompanyName = model.CompanyName,
            Purchase = model.Purchase,
            LastDiv = model.LastDiv,
            Industry = model.Industry,
            MarketCap = model.MarketCap
        };
    } 
}