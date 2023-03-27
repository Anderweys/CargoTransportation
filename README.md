# Cargo Transportation
## Description
This is an application that allows you to arrange the delivery of goods. It was created to consolidate the knowledge gained after studying the material on the Backend and cannot be used in a real project, however, the code is written in such a way that it can be used to write a real project. The idea for creating the project came from reading Eric Evans' book DDD, when it described the implementation of Cargo. Thus, the author wanted to test and hone his skills. <br />
**Important**: When writing the application, the main focus was on the `backend`, which is why the `frontend` turned out so bad. `Frontend` is only needed for backend testing.
## Briefly about the application
The application consists of 9 containers: MSSQL, MongoDb, Redis, RabbitMQ, Cargo.Api, Transportation.Api, Routing.Api, WebMVC, ApiGateway. Each application has its own scope - its own bounded context.
## Stack used
When developing the application, a `microservice architecture` was used 
Also used:
- Architectural patterns: Clear architecture, DDD, CQRS, Event bus.
  - СQRS - MediatR,
  - Event bus - RabbitMQ + MassTransit,
- API Gateway - Ocelot,
- Tests - xUnit.
## Books read before writing the application.
- C# 4.0 The Complete Reference - Herbert Schildt,
- CLR via C# 4th edition - Jeffrey Richter,
- Design Patterns via C# - Alexander Shevchuk, Dmitry Okhrimenko, Andrey Kasyanov,
- Clean Architecture - Robert Martin,
- ASP.NET Core in Action - Andrew Lock,
- Microservices in .NET - Christian Horsdal,
- Domain-Driven Design - Eric Evans,
- Implementing Domain-Driven Design - Vaughn Vernon,
- .NET Microservices: Architecture for Containerized .NET Applications - Microsoft.
## Сode usage
You can safely use the developments from this application for the purpose of self-development!
