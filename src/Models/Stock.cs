using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LondonStockExchangeApi.src.Models
{

    public class Stock
    {
        public string StockSymbol { get; set; }
        public decimal CurrentPrice { get; set; }
        public List<Trade> Trade { get; set; }
    }
}
