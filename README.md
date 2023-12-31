**System Design for London Stock Exchange**
The system is designed with an API First, Cloud native,agnostic approach to include both functional and non-functional requirements. 
Key Components:
1. WebSocket Servers: Facilitate real-time communication(for low latency) between authorized brokers and stock exchange for trade transactions.
2. API Gateway: Single entry point for RESTful API requests, Routes requests to relevant microservice APIs, Enforces authentication/authorization, Caches frequently accessed data, transforms data if required & publishes events to Kafka for trade updates using the POST API.
3. Kafka acts a distributed message broker for buffering trade events by decoupling real-time communication from asynchronous processing.
4. All APIs with the stock exchange business logic are implemented as microservices. Microservices and database components are containerized using Docker and orchestrated by Kubernetes in a cluster.
5. PostgreSQL Database has been chosen for data consistency with performance, scalability and it stores trade data, stock information, broker details, and other relevant data.
6. Monitoring and Alerting are ensured using tools such as CloudMonitor,  CloudTrail, Prometheus, Grafana. Some perfomance metrics include CPU, memory, latency throughput and so on.
7. Robust Security measures such as authentication, authorization and vulnerability management practices are included to protect customer data and also to stay complaint with regulatory and legal requirements for financial systems( GDPR, PCIDSS, etc ).
8. Load balancing, Horizontal scaling, Caching, database indexing could be incorporated to ensure high availability, scalability and fault tolerance.
9. System could be incorporated with CI CD pipeline for efficient deployment and rapid delivery of the applications.
   
Data Flow:
1. Trade requests are initiated by the Brokers via WebSocket servers.
2. API Gateway receives, routes GET requests directly to microservice for retrieving stock prices. But, it forwards POST requests with any transformation if required and publishes to Kafka partitions.
3. Kafka buffers trade events for asynchronous processing and sends published trade events to the POST API (https://lse.com/ms-londonstockexchange-api/api/v1/trades.). This is subscribed to relevant Kafka topics, processes these trade events, Validates data, updates database.
4. Postgresql database stores and manages trade data, stock information, and broker details.

