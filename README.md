# Cargo Transportation
## Description
This is an application that allows you to arrange the delivery of goods. It was created to consolidate the knowledge gained after studying the material on the Backend and cannot be used in a real project, however, the code is written in such a way that it can be used to write a real project. The idea for creating the project came from reading Eric Evans' book DDD, when it described the implementation of Cargo. Thus, the author wanted to test and hone his skills. <br />
**Important**: When writing the application, the main focus was on the `backend`, which is why the `frontend` turned out so bad. `Frontend` is only needed for backend testing.
## Briefly about the application
The application consists of 12 containers: MSSQL, Postgres, MongoDb, Redis, RabbitMQ, Account.API, Cargo.Api, Transportation.Api, Routing.Api, WebMVC, ApiGateway Ocelot and Nginx as proxy. Each application has its own scope - its own bounded context.
## Stack used
When developing the application, a `microservice architecture` was used 
Also used:
- Architectural patterns: Clear architecture, DDD, CQRS, Event bus.
  - СQRS - MediatR,
  - Event bus - RabbitMQ + MassTransit,
- API Gateway - Nginx, Ocelot,
- Tests - xUnit.
## Prerequirements
To run the application, you will need to have free ports. You can see which ports are being used in the `docker-compose.override.yml` file. You also need to have docker-compose pre-installed to run the application.
## Build instructions
First you need to clone the project from the `master` branch.
```
  git clone https://github.com/Anderweys/CargoTransportation.git
```
After the repository has been cloned, go to the `src/CargoTransportation` folder, where the docker files are located and run the command:
```
  docker-compose up -d
```
After that, you need to wait until all the containers are assembled and launched. When this happens, you need to check that the application is working. In the browser, in the address bar you need to write:
```
  http://localhost:5090/
```
If everything works, the start screen should appear. The application generates an account by default for testing: 
- login: `admin`
- password: `123`<br />
Of course, you can also create your own account.<br /><br/>
![CargoTransportation](../master/src/img/Authorization.png)<br/>
After authorization, you should be transferred to the main page.
<br /><br/>
![CargoTransportation](../master/src/img/Mainpage.png)<br/>
Thus, we can conclude that you have successfully launched the application.
## Books read before writing the application
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
You can safely use the developments from this application for the purpose of self-development and for your personal purposes.
