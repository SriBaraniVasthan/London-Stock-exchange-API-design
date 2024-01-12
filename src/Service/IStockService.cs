using LondonStockExchangeApi.src.Models;
using Microsoft.AspNetCore.Mvc;

namespace LondonStockExchangeApi.src.Service
{
    public interface IStockService
    {

        List<Stock> GetAllStocks();
        Stock GetStock(string ticker);
        List<Stock> GetStocksInRange(string[] tickers);
        Task CreateTrade(Trade trade);
    }
}
