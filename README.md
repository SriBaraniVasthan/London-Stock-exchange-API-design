# London-Stock-exchange-System Design 

The system is designed with an API First, Cloud native,agnostic approach to include both functional and non-functional requirements. 
![LSEDesign](https://github.com/SriBaraniVasthan/London-Stock-exchange-API-design/assets/63550126/d86429e6-829a-407a-9b3c-205c689ec9a2)

Key Components:
1. WebSocket Servers: Facilitate real-time communication(for low latency) between authorized brokers and stock exchange for trade transactions.
2. API Gateway: Single entry point for RESTful API requests, Routes requests to relevant microservice APIs, Enforces authentication/authorization, Caches frequently accessed data, transforms data if required & publishes events to Message Queue for trade updates using the POST API.
3. Message Queues acts a distributed message broker for buffering trade events by decoupling real-time communication from asynchronous processing.
4. All APIs with the stock exchange business logic are implemented as microservices. Microservices and database components are containerized using Docker and orchestrated by Kubernetes in a cluster.
5. PostgreSQL Database has been chosen for data consistency with concurrency,performance, scalability and it stores trade data, stock information, broker details, and other relevant data.
6. Monitoring and Alerting are ensured using tools such as CloudMonitor,  CloudTrail, Prometheus, Grafana. Some perfomance metrics include CPU, memory, latency throughput and so on.
7. Robust Security measures such as authentication, authorization and vulnerability management practices are included to protect customer data and also to stay complaint with regulatory and legal requirements for financial systems( GDPR, PCIDSS, etc ).
8. Load balancing, Horizontal scaling, Caching, database indexing could be incorporated to ensure high availability, scalability and fault tolerance.
9. System could be incorporated with CI CD pipeline for efficient deployment and rapid delivery of the applications.

**London-Stock-exchange-API-design**
**REST Endpoints:**
Data Flow:
1. Trade requests are initiated by the Brokers via WebSocket servers.
2. API Gateway receives, routes GET requests directly to microservice for retrieving stock prices. For POST requests, it forwards  with any transformation if required and publishes to Kafka partitions. Kafka buffers trade events for asynchronous processing and sends published trade events to the POST API (sample: https://lse.com/ms-londonstockexchange-api/api/v1/trades.). This is subscribed to relevant partitioned Kafka topics,Validates data, processes these trade events, updates in the database.
   
   a. "api/v1/stocks/trades" is an API that receives exchange of shares from brokers in real-time.
```
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
Possible HTTP Error response Codes:
400 Bad Request (invalid data format)
401 Unauthorized (missing or invalid authentication)
500 Internal Server Error (database or system failure)
```
b. "api/v1/stocks/price/{stock_symbol}" is an API that retrieves the current price of a specific stock.
```
HTTP Method: GET, HTTP Status Code: 200 Created (on success)
URL query Parameters: stock_symbol: The ticker symbol of the stock

API Response:
{
  "stock_symbol": "AAPL",
  "currentValue": 155.75
}
```
c. "api/v1/stocks/prices" is an API that retrieves prices for all stocks on the market.
```
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
```
d. "api/v1/stocks/prices?tickers={stock_symbol1,stock_symbol2,..}" is an API that retrieves prices for a specified list of stock symbols.
```
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
Possible HTTP Error response Codes:
404 Not Found (stock not found)
500 Internal Server Error (database or system failure)
```   
4. Postgresql database stores and manages trade information, stock data, and broker details.

**Postgresql Database**

Entities: Trade, Stock, Broker 
![image](https://github.com/SriBaraniVasthan/London-Stock-exchange-API-design/assets/63550126/c65edda5-9ca5-4d0e-b81a-b463fb9d3646)

1. Broker table
```
Fields:
broker_Id (INT PRIMARY KEY)- One to many relationships with multiple trade transactions,
broker_name (VARCHAR),
email (VARCHAR),
activitystatus (VARCHAR)
```
2. Trade table
```
Fields:
trade_id (INT PRIMARY KEY),
broker_Id (INT FOREIGN KEY REFERENCES Broker(broker_Id)),
stock_symbol (VARCHAR),
order_type (VARCHAR),
price (DECIMAL),
numberOfShares (INT),
timestamp (DATETIME),
orderStatus (VARCHAR) 
```
3. Stock table
```
Fields:
stock_symbol (VARCHAR PRIMARY KEY)- One to many relationships with trade transactions,
current_price (DECIMAL),
```

