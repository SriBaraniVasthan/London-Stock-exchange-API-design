namespace LondonStockExchangeApi.src.Models
{
    public class Trade
    {
        public string TradeId { get; set; }
        public string Stock_Symbol { get; set; }

        public decimal Price { get; set; }

        public decimal NumberOfShares { get; set; }

        public string Broker_Id { get; set; }
        public decimal CurrentValue { get; set; }
        public DateTime timestamp { get; set; }

        public Stock Stock { get; set; }
    }
}
