using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using LondonStockExchangeApi.src.Models;

namespace LondonStockExchangeApi.src.Data
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring
       (DbContextOptionsBuilder options)
        {
            //Uses an inmemory database, but actual postgresql DB can be configured
            options.UseInMemoryDatabase(databaseName: "TradeTxnsDb");
            var context = new ApiContext();
            var conv1 = context.Add(new Stock
            {
                StockSymbol = "AMZN",
                CurrentPrice = 10

            }).Entity;
            var conv2 = context.Stock.Add(new Stock
            {
                StockSymbol = "MSFT",
                CurrentPrice = 10

            }).Entity;

            context.Trade.Add(new Trade
            {
                TradeId = "T1",
                timestamp = DateTimeOffset.UtcNow.DateTime,
                Stock_Symbol = "AMZN",
                NumberOfShares = 10,
                Price = 100,
                Broker_Id = "B1",

            });

            context.Trade.Add(new Trade
            {
                TradeId = "T2",
                timestamp = DateTimeOffset.UtcNow.DateTime,
                Stock_Symbol = "MSFT",
                NumberOfShares = 10,
                Price = 100,
                Broker_Id = "B1",

            });
        }
        public DbSet<Trade> Trade { get; set; }
        public DbSet<Stock> Stock { get; set; }
        // public DbSet<Broker> Broker { get; set; }


    }
}
