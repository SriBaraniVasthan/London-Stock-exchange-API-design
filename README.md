# London-Stock-exchange-API-design

**REST Endpoints:**
1. api/v1/trades is an API that receives exchange of shares from brokers in real-time.
   HTTP Method: POST
   Request body: Content-Type: application/json
Request Body:
{
  "stock_symbol": "AAPL",
  "price": 150.50,
  "numberOfShares": 10.5,
  "broker_Id": "B123"
}

API Response:
HTTP Status Code: 201 Created (on success)
{
  "tradeId": "T123",
   "message": "Trade notification received successfully"
}
Error Codes:
400 Bad Request (invalid data format)
401 Unauthorized (missing or invalid authentication)
500 Internal Server Error (database or system failure)

2. api/v1/stocks/{stock_symbol}/price is an API that retrieves the current price of a specific stock.
HTTP Method: GET, HTTP Status Code: 200 Created (on success)
URL query Parameters: stock_symbol: The ticker symbol of the stock

API Response:
{
  "stock_symbol": "AAPL",
  "currentValue": 155.75
}
Error Codes:
404 Not Found (stock not found)
500 Internal Server Error (database or system failure)

3. api/v1/stocks/prices is an API that retrieves prices for all stocks on the market.
   HTTP Method: GET, HTTP Status Code: 200 Created (on success)
   API Response:
   {
    "price": [
        {
            "stock_symbol": "AAPL",
            "currentValue": 155.75
        },
        {
            "stock_symbol": "GOOGL",
            "currentValue": 1250.1
        },//More stocks
    ]
}
Error Codes:
500 Internal Server Error (database or system failure)

5. api/v1/stocks/prices?tickers={stock_symbol1,stock_symbol2,..} is an API that retrieves prices for a specified list of stock symbols.
Query Parameters: AAPL,GOOGL
HTTP Method: GET
API Response:
[
  {
    "stock_symbol": "AAPL",
    "currentValue": 155.75
  },
  {
    "stock_symbol": "GOOGL",
    "currentValue": 2700.25
  },
  // ... other stocks in the range
]
Error Codes:
500 Internal Server Error (database or system failure)

**Postgresql Database**
Entities: Trade, Stock, Broker 
1. Broker table
Fields:
broker_Id (INT PRIMARY KEY)- One to many relationships with multiple trade transactions,
broker_name (VARCHAR),
email (VARCHAR),
status (VARCHAR)

2. Trade table
Fields:
order_id (INT PRIMARY KEY),
trade_id (INT PRIMARY KEY),
broker_Id (INT FOREIGN KEY REFERENCES Broker(broker_Id)),
stock_symbol (VARCHAR),
order_type (VARCHAR),
price (DECIMAL),
numberOfShares (INT),
timestamp (DATETIME),
orderStatus (VARCHAR) 

3. Stock table
Fields:
stock_symbol (VARCHAR PRIMARY KEY)- One to many relationships with trade transactions,
current_price (DECIMAL),
company_name (VARCHAR),
market_cap (DECIMAL)
