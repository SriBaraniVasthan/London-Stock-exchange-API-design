using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LondonStockExchangeApi.Data;
using System.Diagnostics;
using LondonStockExchangeApi.src.Models;
using LondonStockExchangeApi.src.Service;

namespace LondonStockExchangeApi.src.Controllers
{
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost("api/v1/stocks/trade")]
        public async Task<IActionResult> CreateTrade([FromBody] Trade Txn)
        {

            var result = _stockService.CreateTrade(Txn);

            // Create the response object
            return CreatedAtAction("GetTradeDetails", new { id = Txn.TradeId }, new { tradeId = Txn.TradeId, message = "Trade notification received successfully" });

        }

        // Get
        [HttpGet("api/v1/stocks/prices/{ticker}")]
        public async Task<IActionResult> GetStock(string ticker)
        {
            var result = _stockService.GetStock(ticker);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }
            var response = new
            {
                stock_symbol = result.StockSymbol,
                currentValue = result.CurrentPrice
            };

            return new JsonResult(Ok(response));

        }

        [HttpGet("api/v1/stocks/prices/tickers={tickers}")]
        public async Task<IActionResult> GetStockPrices([FromRoute] string[] tickers)
        {
            var result = _stockService.GetStocksInRange(tickers);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }
            var response = result.Select(stock => new
            {
                stock_symbol = stock.StockSymbol,
                currentValue = stock.CurrentPrice
            }).ToList();
            return new JsonResult(Ok(response));


        }


        // Get all
        [HttpGet("api/v1/stocks/prices")]
        public async Task<IActionResult> GetAllStocks()
        {
            var result = _stockService.GetAllStocks();

            if (result == null)
            {
                return new JsonResult(NotFound());
            }
            var response = new
            {
                price = result.Select(stock => new
                {
                    stock_symbol = stock.StockSymbol,
                    currentValue = stock.CurrentPrice
                }).ToList()
            };

            return new JsonResult(Ok(response));
        }

    }
}
