using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using LondonStockExchangeApi.src.Models;
using LondonStockExchangeApi.src.Data;

namespace LondonStockExchangeApi.src.Service
{
    public class StockService : IStockService
    {

        private readonly ApiContext _context;
        private readonly ILogger Log;
        public StockService(ApiContext context)
        {
            _context = context;
        }
        public List<Stock> GetAllStocks()
        {
            var res = (dynamic)null;
            try
            {
                res = _context.Stock.ToList();

            }
            catch (Exception ex)
            {
                // Handle errors appropriately
                Log.LogError(ex, "An error occurred: {Message}", ex.Message);
            }
            return res;
        }

        public Stock GetStock(string ticker)
        {
            var res = (dynamic)null;
            try
            {
                res = _context.Stock.Find(ticker);

            }
            catch (Exception ex)
            {
                // Handle errors appropriately
                Log.LogError(ex, "An error occurred: {Message}", ex.Message);
            }
            return res;

        }

        public List<Stock> GetStocksInRange(string[] tickers)
        {
            var res = (dynamic)null;
            try
            {
                // Access the ticker values using the tickers array
                res = _context.Stock.Where(s => tickers.Contains(s.StockSymbol)).ToList();

            }
            catch (Exception ex)
            {
                // Handle errors appropriately
                Log.LogError(ex, "An error occurred: {Message}", ex.Message);
            }
            return res;
        }

        public async Task CreateTrade(Trade Txn)
        {
            try
            {
                if (string.IsNullOrEmpty(Txn.TradeId))
                {
                    _context.Trade.Add(Txn);
                }
                else
                {
                    // Check if the trade exists before updating
                    var txnInDb = _context.Trade.FindAsync(Txn.TradeId);

                    if (txnInDb == null)
                        throw new InvalidOperationException("Trade not found");

                    _context.Entry(txnInDb).CurrentValues.SetValues(Txn);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle errors appropriately
                Log.LogError(ex, "An error occurred: {Message}", ex.Message);
            }

        }
    }
}
