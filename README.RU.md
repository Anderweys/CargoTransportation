# Грузоперевозка
## Описание
Это приложение, которое позволяет оформить доставку груза. Оно было создано для закрепления полученных знаний после изучения материала по Backend и не может использоваться в настоящем проекте, однако код написан таким образом, что может быть использован для написания реального проекта. Идея для создания проекта была  придумана при чтении книги Эрика Эванса DDD, когда там описывалась реализация Cargo. Таким образом, автор хотел проверить и отточить свои навыки.<br />
**Важно**: При написании приложения основной упор был на `бэкенд`, поэтому `фронтенд` получился таким плохим. `Фронтенд` нужен только для внутреннего тестирования.
## Коротко о приложении
Приложение состоит из 9 контейнеров: MSSQL, MongoDb, Redis, RabbitMQ, Cargo.Api, Transportation.Api, Routing.Api, WebMVC, ApiGateway. 
Каждое  приложение имеет свою область - свой ограниченный контекст.
## Используемый стек
При разработке приложения использовалась `микросервисная архитектура`.
А также использованы:
- Архитектурные шаблоны: Clear architecture, DDD, CQRS, Event bus.
  - СQRS - MediatR,
  - Event bus - RabbitMQ + MassTransit,
- API Шлюз - Ocelot,
- Тестирование - xUnit.
## Книги, прочтенные перед разработкой приложения
- C# 4.0 The Complete Reference - Herbert Schildt,
- CLR via C# 4th edition - Jeffrey Richter,
- Design Patterns via C# - Alexander Shevchuk, Dmitry Okhrimenko, Andrey Kasyanov,
- Clean Architecture - Robert Martin,
- ASP.NET Core in Action - Andrew Lock,
- Microservices in .NET - Christian Horsdal,
- Domain-Driven Design - Eric Evans,
- Implementing Domain-Driven Design - Vaughn Vernon,
- .NET Microservices: Architecture for Containerized .NET Applications - Microsoft.
## Использование кода
Вы можете смело использовать наработки из этого приложения в целях саморазвития!
