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
            MarketCap = model.MarketCap,
            Comments = model.Comments.Select(c => c.ToCommentDto()).ToList(),
        };
    }

    public static Stock CreateStockRequestDto(this CreateStockDto stockDto)
    {
        return new Stock
        {
            Symbol = stockDto.Symbol,
            CompanyName = stockDto.CompanyName,
            Purchase = stockDto.Purchase,
            LastDiv = stockDto.LastDiv,
            Industry = stockDto.Industry,
            MarketCap = stockDto.MarketCap
        };
    }
}