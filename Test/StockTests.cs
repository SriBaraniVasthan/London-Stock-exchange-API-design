using LondonStockExchangeApi.src.Controllers;
using LondonStockExchangeApi.src.Models;
using LondonStockExchangeApi.src.Service;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;

namespace LondonStockExchangeApi.Test
{

    [TestFixture]
    public class StockTests
    {
        private Mock<IStockService> _stockServiceMock;
        private StockController _controller;

        [SetUp]
        public void Setup()
        {
            _stockServiceMock = new Mock<IStockService>();
            _controller = new StockController(_stockServiceMock.Object);
        }

        [Test]
        public async Task GetValidStockPrices()
        {
            // Arrange
            string[] tickers = new string[] { "AAPL", "GOOGL" };
            List<Stock> mockStocks = new List<Stock>
        {
            new Stock { StockSymbol = "AAPL", CurrentPrice = 150.50 },
            new Stock { StockSymbol = "GOOGL", CurrentPrice = 200.00 }
        };
            _stockServiceMock.Setup(s => s.GetAllStocks().ReturnsAsync(mockStocks));

            // Act
            var response = await _controller.GetStockPrices(tickers);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(response.Value, Is.InstanceOf<IEnumerable<Stock>>());
            var stocks = response.Value as IEnumerable<Stock>;
            Assert.That(stocks.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetStockPrices()
        {
            // Arrange
            string[] tickers = new string[] { "XYZ" };
            _stockServiceMock.Setup(s => s.GetStocksInRange(It.IsAny<string[]>())).ReturnsAsync(Enumerable.Empty<Stock>());

            // Act
            var response = await _controller.GetStockPrices(tickers);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }

        [Test]
        public void CreateTrade()
        {
            // Arrange
            var trade = new Trade { TradeId = "T1", Stock_Symbol = "AAPL", NumberOfShares = 10, Price = 150.50m };

            // Act
            _controller.CreateTrade(trade);

            // Assert
            _stockServiceMock.Verify(s => s.CreateTrade(It.IsAny<Trade>()), Times.Once);
        }
        [Test]
        public async Task GetAllStocks()
        {
            // Arrange
            var mockStocks = new List<Stock>
    {
        new Stock { StockSymbol = "AAPL" },
        new Stock { StockSymbol = "MSFT" }
    };
            _contextMock.Setup(c => c.Stock).Returns(mockStocks.AsQueryable());

            // Act
            var result = await _stockServiceMock.GetAllStocks();

            // Assert
            Assert.AreEqual(mockStocks, result);
        }
    }

}
