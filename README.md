# Clubs
Clubs is a project sample that can be used as a service in a microservice system.
This project required [.Net 5](https://dotnet.microsoft.com/download/dotnet/5.0) to be installed.
This project use SQLite for database persistence

## Test
```bash
cd Clubs.UnitTest
dotnet test
```

## Installation
```bash
cd Clubs.Api
dotnet restore
dotnet run
```

## Microservices interservice communication
In a microservices system, each service is independent and has it's own business logic and data. 
But they still need to intergate with each others.

There's two type of communication:
- Synchronous protocol (like HTTP) : Client code send a request and wait for the response.
- Asynchronous protocol (like messaging): Client code send a request but doesn't wait for the response.

In my opinion, the option 2 is better because each service is more independent. With synchronous communication, if one service fails or not working, others services also hang and can neither function nor resposne to customer. The whole system will not be resilient.

With asynchronous communication, if one service is off, other services can still work and transfer the works to the message broker (like rabbitMQ) , and the offline services can continue their works when they are online again.

With synchronous communication, the services will become a chain of services, and customer will have to wait for all the services to complete before they can receive the response. So the performance and user experience will suffer.

So the asynchronous protocol is more resilient, fault-tolerant and improve the availability of the system. We should alway use asyschronous whenever possible.  



