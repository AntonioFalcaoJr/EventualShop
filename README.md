# EDA.CleanArch.DDD.CQRS.EventSourcing

Note. _Greg Young takes the next steps beyond the DDD principles and best practices introduced by Eric Evans in **Domain-Driven Design: Tackling Complexity in the Heart of Software**, using DDD with **Command-Query Responsibility Segregation** (CQRS) and **event sourcing** to simplify construction, decentralize decision-making, and make system development more flexible and responsive._ Adapted from "Event Centric: Finding Simplicity in Complex Systems" by Y. Greg, 2012.

This project uses the **EventStorming** workshop to identify the business capabilities and the respective **Bounded Contexts** of a simple e-commerce, as well as the integration events that occur between them. In addition to demonstrating the implementation under an **Event-driven architecture** (EDA), through an **event-sourcing** design supported by the **CQRS** pattern in a **Clean Architecture**.

> State transitions are an important part of our problem space and should be modelled within our domain.    
> -- <cite> Greg Young </cite>

### Give a Star! :star:

## Event-driven architecture (EDA)
> Event-driven architecture (EDA) is a software architecture paradigm promoting the production, detection, consumption of, and reaction to events. An event can be defined as "a significant change in state".      
> 
> "Event-driven architecture." *Wikipedia*, Wikimedia Foundation, last edited on 9 May 2021.  
> https://en.wikipedia.org/wiki/Event-driven_architecture

 ![](./.assets/img/eda.png)    
 Fig. 1: Uit de Bos, Oskar. *A simple illustration of events using the publish/subscribe messaging model*.    
 https://medium.com/swlh/the-engineers-guide-to-event-driven-architectures-benefits-and-challenges-3e96ded8568b

<br>

The following table shows how EDA and Microservices architectural styles compliment each other:

| EDA | Microservices Architecture |
|---|---|
| Loose coupling between components/services | Bounded context which provides separation of concerns |
| Ability to scale individual components | Independently deployable & scalable |
| Processing components can be developed independent of each other | Support for polyglot programming |
| High cloud affinity | Cloud native |
| Asynchronous nature. As well as ability to throttle workload | Elastic scalability |
| Fault Tolerance and better resiliency | Good observability to detect failures quickly |
| Ability to build processing pipelines | Evolutionary in nature |
| Availability of sophisticated event brokers reduce code complexity | Set of standard reusable technical services often referred as `MicroServices Chassis`  |
| A rich palate of proven [Enterprise Integration Patterns](https://www.enterpriseintegrationpatterns.com/) | Provides a rich repository of reusable [implementation patterns](https://microservices.io/patterns/microservices.html) |
  
Table 1: Ambre, Tanmay. *Architectural considerations for event-driven microservices-based systems*.    
https://developer.ibm.com/articles/eda-and-microservices-architecture-best-practices/

## CQRS
> CQRS stands for Command and Query Responsibility Segregation, a pattern that separates read and update operations for a data store. Implementing CQRS in your application can maximize its performance, scalability, and security. The flexibility created by migrating to CQRS allows a system to better evolve over time and prevents update commands from causing merge conflicts at the domain level.
>
> "What is the CQRS pattern?" *MSDN*, Microsoft Docs, last edited on 2 Nov 2020.  
> https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs

<br>

 ![](.assets/img/cqrs.png)   
 Fig. 2: Bürckel, Marco. *Some thoughts on using CQRS without Event Sourcing*.    
 https://medium.com/@mbue/some-thoughts-on-using-cqrs-without-event-sourcing-938b878166a2

### Projections
To cover this topic was prepared [this presentation](https://www.canva.com/design/DAEY9ttmPgY/F_lh7TXQEdG-su-qojEjdw/view?utm_content=DAEY9ttmPgY&utm_campaign=designshare&utm_medium=link&utm_source=publishsharelink) with some different strategies and ways to implement projections.

## Event sourcing

> Instead of storing just the current state of the data in a domain, use an append-only store to record the full series of actions taken on that data. The store acts as the system of record and can be used to materialize the domain objects. This can simplify tasks in complex domains, by avoiding the need to synchronize the data model and the business domain, while improving performance, scalability, and responsiveness. It can also provide consistency for transactional data, and maintain full audit trails and history that can enable compensating actions.
>
> "Event Sourcing pattern" *MSDN*, Microsoft Docs, last edited on 23 Jun 2017.  
> https://docs.microsoft.com/en-us/azure/architecture/patterns/event-sourcing

> We can query an application's state to find out the current state of the world, and this answers many questions. However there are times when we don't just want to see where we are, we also want to know how we got there.
> 
> Event Sourcing ensures that all changes to application state are stored as a sequence of events. Not just can we query these events, we can also use the event log to reconstruct past states, and as a foundation to automatically adjust the state to cope with retroactive changes.
>
> Fowler Martin, 2005, *Event Sourcing*.   
> https://martinfowler.com/eaaDev/EventSourcing.html

<br>

![](./.assets/img/event-sourcing-overview.png)  
Fig. 3: MSDN. *Event Sourcing pattern*.    
https://microservices.io/patterns/data/event-sourcing.html

<br>

![](./.assets/img/event-sourcing.png)
Fig. 4: Richardson, Chris. *Pattern: Event sourcing*.    
https://microservices.io/patterns/data/event-sourcing.html

## CQRS + Event-sourcing
> CQRS and Event Sourcing have a symbiotic relationship. CQRS allows Event Sourcing to be used as the
data storage mechanism for the domain.  
> 
> Young Greg, 2012, *CQRS and Event Sourcing*, **CQRS Documents by Greg Young**, p50. 

> The CQRS pattern is often used along with the Event Sourcing pattern. CQRS-based systems use separate read and write data models, each tailored to relevant tasks and often located in physically separate stores. When used with the Event Sourcing pattern, the store of events is the write model, and is the official source of information. The read model of a CQRS-based system provides materialized views of the data, typically as highly denormalized views. These views are tailored to the interfaces and display requirements of the application, which helps to maximize both display and query performance.
>
> "Event Sourcing and CQRS pattern" *MSDN*, Microsoft Docs, last edited on 02 Nov 2020.   
> https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs#event-sourcing-and-cqrs-pattern

 ![](./.assets/img/cqrs-eventsourcing-diagram.png)
 Fig. 5: Whittaker, Daniel. *CQRS + Event Sourcing – Step by Step*.    
 https://danielwhittaker.me/2020/02/20/cqrs-step-step-guide-flow-typical-application/

<br>

![](./.assets/img/cqrs-eventsourcing-flow.png)  
Fig. 6: Katwatka, Piotr. *Event Sourcing with CQRS*.  
https://www.divante.com/blog/event-sourcing-open-loyalty-engineering

## EventStorming

>EventStorming is a flexible workshop format for collaborative exploration of complex business domains.
>
>It comes in different flavours, that can be used in different scenarios:
>
> * to assess health of an existing line of business and to discover the most effective areas for improvements;
> * to explore the viability of a new startup business model;
> * to envision new services, that maximise positive outcomes to every party involved;
> * to design clean and maintainable Event-Driven software, to support rapidly evolving businesses.
>
> The adaptive nature of EventStorming allows sophisticated cross-discipline conversation between stakeholders with different backgrounds, delivering a new type of collaboration beyond silo and specialisation boundaries.
>
> "EventStorming", *EventStorming.com*, last edited on 2020.   
> https://www.eventstorming.com/

![](./.assets/img/event-storming.jpg)  
Fig. 7: Baas-Schwegler, Kenny & Richardson, Chris. *Picture that explains "Almost" Everything*.    
https://github.com/ddd-crew/eventstorming-glossary-cheat-sheet

### EventStorming (WIP)
![](./.assets/img/event-storming-wip.jpg)

## Clean Architecture

> Clean architecture is a software design philosophy that separates the elements of a design into ring levels. An important goal of clean architecture is to provide developers with a way to organize code in such a way that it encapsulates the business logic but keeps it separate from the delivery mechanism.
>
> The main rule of clean architecture is that code dependencies can only move from the outer levels inward. Code on the inner layers can have no knowledge of functions on the outer layers. The variables, functions and classes (any entities) that exist in the outer layers can not be mentioned in the more inward levels. It is recommended that data formats also stay separate between levels.
>
> "Clean Architecture." *Whatis*, last edited on 10 Mar 2019.  
> https://whatis.techtarget.com/definition/clean-architecture


 ![](./.assets/img/CleanArchitecture.jpg)  
 Fig. 8: C. Martin, Robert. *The Clean Architecture*.    
 https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html


## Running

### Development (secrets)

To configure database resource, `init` secrets in [`./src/WebAPI`](./src/WebAPI), and then define the `DefaultConnection` in `ConnectionStrings` options:

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=<IP_ADDRESS>,1433;Database=EventStore;User=sa;Password=!MyStrongPassword"
```

##### AppSettings

If prefer, define it on WebAPI [`appsettings.Development.json`](./src/WebAPI/appsettings.Development.json) file:

```json5
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<IP_ADDRESS>,1433;Database=Store;User=sa;Password=!MyComplexPassword"
  }
}
```
### Production

Considering use Docker for CD (Continuous Deployment). On respective [compose](./docker-compose.yml) the **web application** and **sql server** are in the same network, and then we can use named hosts. Already defined on WebAPI [`appsettings.json`](./src/Dotnet6.GraphQL4.Store.WebAPI/appsettings.json) and WebMVC [`appsettings.json`](./src/Dotnet6.GraphQL4.Store.WebMVC/appsettings.json) files:

#### AppSettings

WebAPI

```json5
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=mssql;Database=EventStore;User=sa;Password=!MyStrongPassword"
  }
}
```
### Docker

The [`./docker-compose.yml`](./docker-compose.yml) provide the `WebAPI` and `MS SQL Server`:

```bash
docker-compose up -d
``` 

It's possible to run without a clone of the project using the respective compose:

```yaml
version: "3.7"

services:
  mssql:
    container_name: mssql
    image: mcr.microsoft.com/mssql/server
    ports:
      - 1433:1433
    environment:
      SA_PASSWORD: "!MyStrongPassword"
      ACCEPT_EULA: "Y"
    healthcheck:
      test: /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$$SA_PASSWORD" -Q "SELECT 1" || exit 1
      interval: 10s
      timeout: 3s
      retries: 10
      start_period: 10s
    networks:
      - eventstore

  webapi:
    container_name: webapi
    image: antoniofalcaojr/dotnet-cleanarch-cqrs-eventsourcing-webapi
    environment:
      - ASPNETCORE_URLS=http://*:5000
    ports:
      - 5000:5000
    depends_on:
      mssql:
        condition: service_healthy
    networks:
      - eventstore

  healthchecks:
    container_name: healthchecks-ui
    image: xabarilcoding/healthchecksui
    depends_on:
      mssql:
        condition: service_healthy
    environment:
      - storage_provider=SqlServer
      - storage_connection=Server=mssql;Database=EventStore;User=sa;Password=!MyStrongPassword
      - Logging:LogLevel:Default=Debug
      - Logging:Loglevel:Microsoft=Warning
      - Logging:LogLevel:HealthChecks=Debug
      - HealthChecksUI:HealthChecks:0:Name=webapi
      - HealthChecksUI:HealthChecks:0:Uri=http://webapi:5000/healthz
    ports:
      - 8000:80
    networks:
      - eventstore

networks:
  eventstore:
    driver: bridge
```

Docker commands

MSSQL
```zsh
docker run -d \
-e 'ACCEPT_EULA=Y' \
-e 'SA_PASSWORD=!MyStrongPassword' \
-p 1433:1433 \
--name mssql \
mcr.microsoft.com/mssql/server
```
MongoDB
```zsh
docker run -d \
-e 'MONGO_INITDB_ROOT_USERNAME=mongoadmin' \
-e 'MONGO_INITDB_ROOT_PASSWORD=secret' \
-p 27017:27017 \
--name mongodb \
mongo
```
RabbitMQ
```zsh
docker run -d \ 
-p 15672:15672 \
-p 5672:5672 \
--hostname my-rabbit \
--name rabbitmq \
rabbitmq:3-management
```

## Event store

```sql
CREATE TABLE [CustomerStoreEvents] (
    [Id] int NOT NULL IDENTITY,
    [AggregateId] uniqueidentifier NOT NULL,
    [AggregateName] varchar(30) NOT NULL,
    [EventName] varchar(50) NOT NULL,
    [Event] varchar(1000) NOT NULL,
    CONSTRAINT [PK_CustomerStoreEvents] PRIMARY KEY ([Id])
);
```

```json
{
  "$type": "Domain.Entities.Customers.Events+NameChanged, Domain",
  "Name": "string",
  "Timestamp": "2021-07-12T14:22:23.2600385-03:00"
}
```
RabbitMQ/MassTransit

Commands
```ini
Queue: customer-registered, Consumer: Application.UseCases.Customers.EventHandlers.CustomerRegistered.CustomerRegisteredConsumer
Queue: customer-age-changed, Consumer: Application.UseCases.Customers.EventHandlers.CustomerUpdated.CustomerUpdatedConsumer
Queue: customer-name-changed, Consumer: Application.UseCases.Customers.EventHandlers.CustomerUpdated.CustomerUpdatedConsumer
Queue: customer-deleted, Consumer: Application.UseCases.Customers.EventHandlers.CustomerDeleted.CustomerDeletedConsumer
```

Events
```ini
Queue: update-customer, Consumer: Application.UseCases.Customers.Commands.UpdateCustomer.UpdateCustomerConsumer
Queue: register-customer, Consumer: Application.UseCases.Customers.Commands.RegisterCustomer.RegisterCustomerConsumer
Queue: delete-customer, Consumer: Application.UseCases.Customers.Commands.DeleteCustomer.DeleteCustomerConsumer**
```

Queries
```ini
Queue: get-customers-details-with-pagination-query, Consumer: Application.UseCases.Customers.Queries.GetCustomersWithPagination.GetCustomersDetailsWithPaginationQueryConsumer
Queue: get-customer-detail-query, Consumer: Application.UseCases.Customers.Queries.GetCustomerDetails.GetCustomerDetailQueryConsumer
```
## References

* [Event Centric: Finding Simplicity in Complex Systems](https://www.amazon.com/Event-Centric-Simplicity-Addison-Wesley-Signature/dp/0321768221)
* [CQRS Documents - Greg Young](https://cqrs.files.wordpress.com/2010/11/cqrs_documents.pdf)
* [Versioning in an Event Sourced - Greg Young](https://leanpub.com/esversioning/read)
* [Pattern: Event sourcing - Chris Richardson](https://microservices.io/patterns/data/event-sourcing.html)
* [Clarified CQRS - Udi Dahan](https://udidahan.com/2009/12/09/clarified-cqrs/)
* [Udi & Greg Reach CQRS Agreement](https://udidahan.com/2012/02/10/udi-greg-reach-cqrs-agreement/)
* [Event Sourcing and CQRS - Alexey Zimarev](https://www.eventstore.com/blog/event-sourcing-and-cqrs)
* [What is Event Sourcing? - Alexey Zimarev](https://www.eventstore.com/blog/what-is-event-sourcing)
* [Transcript of Greg Young's Talk at Code on the Beach 2014: CQRS and Event Sourcing](https://www.eventstore.com/blog/transcript-of-greg-youngs-talk-at-code-on-the-beach-2014-cqrs-and-event-sourcing)
* [Introduction to CQRS - Kanasz Robert](https://www.codeproject.com/Articles/555855/Introduction-to-CQRS)
* [Distilling the CQRS/ES Capability - Vijay Nair](https://axoniq.io/blog-overview/distilling-the-cqrses-capability)
* [Dispelling the Eventual Consistency FUD when using Event Sourcing - Vijay Nair](https://axoniq.io/blog-overview/dispelling-the-eventual-consistency-fud-when-using-event-sourcing)
* [Why would I need a specialized Event Store? - Greg Woods](https://axoniq.io/blog-overview/eventstore)
* [A Fast and Lightweight Solution for CQRS and Event Sourcing - Daniel Miller](https://www.codeproject.com/Articles/5264244/A-Fast-and-Lightweight-Solution-for-CQRS-and-Event)
* [Event Sourcing: The Good, The Bad and The Ugly - Dennis Doomen](https://www.continuousimprover.com/2017/11/event-sourcing-good-bad-and-ugly.html)
* [What they don’t tell you about event sourcing - Hugo Rocha](https://medium.com/@hugo.oliveira.rocha/what-they-dont-tell-you-about-event-sourcing-6afc23c69e9a)
* [Event Sourcing pattern - MSDN](https://docs.microsoft.com/en-us/azure/architecture/patterns/event-sourcing)
* [CQRS + Event Sourcing, Step by Step - Daniel](https://danielwhittaker.me/2020/02/20/cqrs-step-step-guide-flow-typical-application/)
* [Architectural considerations for event-driven microservices-based systems - Tanmay Ambre](https://developer.ibm.com/articles/eda-and-microservices-architecture-best-practices/)
* [How messaging simplifies and strengthens your microservice application - Callum Jackson](https://developer.ibm.com/articles/how-messaging-simplifies-strengthens-microservice-applications/)