# EDA.CleanArch.DDD.CQRS.EventSourcing

Note. _Greg Young takes the next steps beyond the DDD principles and best practices introduced by Eric Evans in **Domain-Driven Design: Tackling Complexity in the Heart of Software**, using DDD
with **Command-Query Responsibility Segregation** (CQRS) and **event sourcing** to simplify construction, decentralize decision-making, and make system development more flexible and responsive._
Adapted from "Event Centric: Finding Simplicity in Complex Systems" by Y. Greg, 2012.

This project applies the **EventStorming** workshop to decompose and aggregate business capabilities in **Bounded Contexts** of a simple e-commerce, as well as the **Domain Events** that
provide integration between them. In addition to demonstrating implementation under a reactive **Event-driven architecture** (EDA), through the **event-sourcing** design, supported by the **CQRS**
pattern, in a **Clean Architecture**.

> State transitions are an important part of our problem space and should be modelled within our domain.    
> -- <cite> Greg Young </cite>

### Give a Star! :star:

If this project helped you in some way, please **give it a star**. Thanks!

---

| Service      |                                                                                                                             Build & Test                                                                                                                              |  Security  |  Quality  |
|--------------|:---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:----------:|:---------:|
| Account      |       [![](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/actions/workflows/account-build-and-test.yaml/badge.svg)](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/actions/workflows/account-build-and-test.yaml)       |            |           |
| Catalog      |       [![](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/actions/workflows/catalog-build-and-test.yaml/badge.svg)](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/actions/workflows/catalog-build-and-test.yaml)       |            |           |
| Identity     |      [![](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/actions/workflows/identity-build-and-test.yaml/badge.svg)](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/actions/workflows/identity-build-and-test.yaml)      |            |           | 
| Order        |         [![](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/actions/workflows/order-build-and-test.yaml/badge.svg)](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/actions/workflows/order-build-and-test.yaml)         |            |           |
| Payment      |       [![](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/actions/workflows/payment-build-and-test.yaml/badge.svg)](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/actions/workflows/payment-build-and-test.yaml)       |            |           |
| ShoppingCart |  [![](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/actions/workflows/shoppingcart-build-and-test.yaml/badge.svg)](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/actions/workflows/shoppingcart-build-and-test.yaml)  |            |           |
| Warehouse    |     [![](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/actions/workflows/warehouse-build-and-test.yaml/badge.svg)](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/actions/workflows/warehouse-build-and-test.yaml)     |            |           |

## Roadmap

- **Context Mapping** considering different types of relationships (upstream/downstream):
    - partnership;
    - customer-supplier;
    - conformist.

- **Idempotent Consumer Filter** to prevent intentions from being expressed repeatedly:
    - Do not consider duplicate messages;
    - Do not consider naturally idempotent intentions;
    - Consider the business expected interval in idempotence hash calculation.

- **Outbox pattern** to provide distributed resilient publishing:
    - Wrap publisher to persist just in case of failure;
    - ![](./.assets/img/outbox.jpg)

- **RPC Read Stack** (gRPC)
    - V2 BFF/Gateway route;
    - Proto files as contracts.

- _SPIKE_: **Controller to Minimal API** 
  - Based on cohesion from [ApplicationController](./src/Web/WebAPI/Abstractions/ApplicationController.cs), consider moving design from Gateway/BFF to Minimal one.

## The Solution Architecture

![](./.assets/img/solution_architecture.png)    
Fig. 1: Falcão Jr., Antônio. *An EDA solution architecture*.

## Messaging - Making good use of Context Mapping

![](./.assets/img/messaging_context_mapping.png)  
Fig. 3: Vernon, V. (2016), Messaging from Domain-Driven Design Distilled, 1st ed, p65.

> When using asynchronous messaging to integrate, much can be accomplished by a client Bounded Context subscribing to the Domain Events published by your own or another Bounded Context. Using
> messaging is one of the most robust forms of integration because you remove much of the temporal coupling associated with blocking forms such as RPC and REST. Since you already anticipate the latency
> of message exchange, you tend to build more robust systems because you never expect immediate results.
>
> Typically an Aggregate in one Bounded Context publishes a Domain Event, which could be consumed by any number of interested parties. When a subscribing Bounded Context receives the Domain Event,
> some action will be taken based on its type and value. Normally it will cause a new Aggregate to be created or an existing Aggregate to be modified in the consuming Bounded Context.
>
> Vernon, V. (2016) Domain-Driven Design Distilled, 1st ed. New York: Addison-Wesley Professional, pp65-67.

## Reactive Domain Drive Design

> I have been seeing, at least in my world, a trend towards reactive systems. Not just reactive within a microservice, but building entire systems that are reactive. In DDD, reactive behavior is also
> happening within the bounded context. Being reactive isn't entirely new, and Eric Evans was far ahead of the industry when he introduced eventing. Using domain events means we have to react to
> events that happened in the past, and bring our system into harmony.
>
> Vernon, Vaughn. "Modeling Uncertainty with Reactive DDD", *www.infoq.com*, last edited on 29 Set 2018.  
> https://www.infoq.com/articles/modeling-uncertainty-reactive-ddd/

![](./.assets/img/reactive-system.jpg)  
Fig. 3: Vernon, V. (2018), "Modeling Uncertainty with Reactive DDD", *Reactive Systems*, www.infoq.com.   
https://www.infoq.com/articles/modeling-uncertainty-reactive-ddd/

## Reactive Process

> Each domain entity is responsible for tracking its state, based on the commands it receives. By following good DDD practices, the state can be safely tracked based on these commands, and using event
> sourcing to persist the state change events.
>
> This is where we want to be. When everything is happening asynchronously everywhere, what happens? That brings us to uncertainty.
>
> If there is any possibility of any message being out of order, you have to plan for all of them being out of order.
>
> Each entity is also responsible for knowing how to handle any potential uncertainty, according to decisions made by domain experts. For example, if a duplicate event is received, the aggregate will
> know that it has already seen it, and can decide how to respond.
>
> Vernon, Vaughn. "Modeling Uncertainty with Reactive DDD", *www.infoq.com*, last edited on 29 Set 2018.  
> https://www.infoq.com/articles/modeling-uncertainty-reactive-ddd/

![](./.assets/img/reactive_process.jpg)  
Fig. 3: Vernon, V. (2018), "Modeling Uncertainty with Reactive DDD", *Process Manager*, www.infoq.com.   
https://www.infoq.com/articles/modeling-uncertainty-reactive-ddd/

## Temporal Coupling and Autonomous Decisions

> Temporal coupling is where you have a dependency on time where one service or one component cannot complete its operation until the other party is done with work. In order to get rid of this
> temporal coupling, what you can do is you can use events.
>
> Alagarsamy, Indu. "Practical DDD: Bounded Contexts + Events => Microservices", *www.infoq.com*, last edited on 03 Set 2019.  
> https://www.infoq.com/presentations/microservices-ddd-bounded-contexts

## Event-driven architecture (EDA)

> Event-driven architecture (EDA) is a software architecture paradigm promoting the production, detection, consumption of, and reaction to events. An event can be defined as "a significant change in
> state".
>
> "Event-driven architecture." *Wikipedia*, Wikimedia Foundation, last edited on 9 May 2021.  
> https://en.wikipedia.org/wiki/Event-driven_architecture

> Event-driven architecture refers to a system of loosely coupled microservices that exchange information between each other through the production and consumption of events. An event-driven system
> enables messages to be ingested into the event driven ecosystem and then broadcast out to whichever services are interested in receiving them.
>
> Jansen, Grace & Saladas, Johanna. "Advantages of the event-driven architecture pattern." *developer.ibm.com*, IBM Developer, last edited on 12 May 2021.  
> https://developer.ibm.com/articles/advantages-of-an-event-driven-architecture/

![](./.assets/img/eda.png)    
Fig. 4: Uit de Bos, Oskar. *A simple illustration of events using the publish/subscribe messaging model*.    
https://medium.com/swlh/the-engineers-guide-to-event-driven-architectures-benefits-and-challenges-3e96ded8568b

## Microservices

> The term "Microservice Architecture" has sprung up over the last few years to describe a particular way of designing software applications as suites of independently deployable services. While there
> is no precise definition of this architectural style, there are certain common characteristics around organization around business capability, automated deployment, intelligence in the endpoints, and
> decentralized control of languages and data.
>
> Fowler, Martin. "Microservices", *martinfowler.com*, last edited on 25 Mar 2014.     
> https://martinfowler.com/articles/microservices.html

### EDA & Microservices Architecture

The following table shows how EDA and Microservices architectural styles compliment each other:

| EDA                                                                                                       | Microservices Architecture                                                                                             |
|-----------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------|
| Loose coupling between components/services                                                                | Bounded context which provides separation of concerns                                                                  |
| Ability to scale individual components                                                                    | Independently deployable & scalable                                                                                    |
| Processing components can be developed independent of each other                                          | Support for polyglot programming                                                                                       |
| High cloud affinity                                                                                       | Cloud native                                                                                                           |
| Asynchronous nature. As well as ability to throttle workload                                              | Elastic scalability                                                                                                    |
| Fault Tolerance and better resiliency                                                                     | Good observability to detect failures quickly                                                                          |
| Ability to build processing pipelines                                                                     | Evolutionary in nature                                                                                                 |
| Availability of sophisticated event brokers reduce code complexity                                        | Set of standard reusable technical services often referred as `MicroServices Chassis`                                  |
| A rich palate of proven [Enterprise Integration Patterns](https://www.enterpriseintegrationpatterns.com/) | Provides a rich repository of reusable [implementation patterns](https://microservices.io/patterns/microservices.html) |

Table 1: Ambre, Tanmay. *Architectural styles compliment*, Architectural considerations for event-driven microservices-based systems.    
https://developer.ibm.com/articles/eda-and-microservices-architecture-best-practices/

### EDA vs SOA

> Compared to SOA, the essence of an EDA is that the services involved communicate through the capture, propagation, processing and persistence of events. This resulting pattern of communicating
> through a dataflow is quite different from the SOA approach of requests and responses.
>
> Mathew, Jerry. "SOA vs. EDA: Is Not Life Simply a Series of Events?." *Confluent.io*, last edited on 19 Mar 2019.  
> https://www.confluent.io/blog/soa-vs-eda-is-not-life-simply-a-series-of-events/

According to Mathew, here are some reasons why the EDA patterns can alleviate some of the challenges traditional SOA patterns bring:

|                                                                          | SOA                                                                                                                                                                                                                                                                                                                                                                         | EDA                                                                                                                                                                                                                                                                                                                                                                              |
|--------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Pull vs. Reactive                                                        | Client makes a request of a service and expects a response. It’s great for persisted, static data, but gets a little hard when data keeps changing. You have to poll to detect changes.                                                                                                                                                                                     | Subscription model pushes events to consumers.                                                                                                                                                                                                                                                                                                                                   |
| Coupling                                                                 | Client has to know details of the API and its location at runtime.                                                                                                                                                                                                                                                                                                          | Producers have no knowledge of consumer which will ultimately receive the event. There is still some minimal coupling in terms names of queues/topics and event formats.                                                                                                                                                                                                         |
| Service Availability                                                     | A service must be available at the time a request is made by a client even if you are doing an asynchronous response handling.                                                                                                                                                                                                                                              | Events do not require a reply and are inherently asynchronous. Events can be persisted for future consumption. With a highly fault-tolerant broker, the event producer does not need to know whether the consumers are available. Thus, we achieve higher resilience to network and compute failure, and this allows event producers to avoid blocking.                          |
| Process Modification and Extension                                       | Processing logic is a request-response API that is hardwired into a service endpoint (with or without service discovery). If the logic needs to change or be extended, or if new logic needs to be introduced, the definition (not contract) of the service must be updated. This introduces change management and regression risk.                                         | Additional event producers and consumers can be added to a system without any explicit process definition.                                                                                                                                                                                                                                                                       |
| Consistency Between Process Interaction and Internal State Management    | State changes are managed based on requests. For example, a request to “withdraw money” mutates the state of an account. The distinct processes of a request, a change in state and its persistence in case of failure must be tied together transactionally. This often leads users to deploy expensive distributed transaction protocols like eXtended architecture (XA). | EDA provides better support for consistency between process interaction and persisted internal state transitions. This is done through the event sourcing pattern, where the communication protocol (the event) is also the persistence mechanism (the event log). The current state of a system can be built or rebuilt from the log of events.                                 |
| Retaining the Exact State Transitions That Customers or Services Perform | In SOAs, data is typically “mutated in place” in a database. This is a lossy process where each state change loses the information about the state changes that happened previously.                                                                                                                                                                                        | EDAs are event sourced, meaning every state change is captured, providing a truthful journal of the exact state changes that every customer or every service made over time. This journal lets operators rewind time to view or replay previous events exactly as they happened. It is also important for analytics that review customer (or system) behavior to derive insight. |
| *Streaming Analytics                                                     | SOA is incapable of deriving analytics of data in flight. This requires the ability to detect a pattern from multiple state changes both temporally and spatially.                                                                                                                                                                                                          | EDA is fully capable of detecting patterns across multiple event sources over many different types of time windows. Also, deriving analytics of data in flight is a means of continuous intelligence.                                                                                                                                                                            |
| The Timing of Consistency and of Intelligence                            | Synchronous communication makes it a bit easier to create consistent state across services from a client’s perspective. Intelligence from the consistent state are derived eventually—that is, eventual intelligence and continuous consistency                                                                                                                             | Events, being asynchronous, mean that different services become consistent with one another only in eventuality: There is no control over the timeliness of the process of event propagation.                                                                                                                                                                                    |

Table 2: Mathew, Jerry. *SOA vs. EDA: Is Not Life Simply a Series of Events?*  
https://www.confluent.io/blog/soa-vs-eda-is-not-life-simply-a-series-of-events/

## Command's pipeline

![](./.assets/img/add-ha-message-queue.png)    
Fig. 2: MSDN. *Use message queues (out-of-proc) in the command's pipeline*.    
https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api#use-message-queues-out-of-proc-in-the-commands-pipeline

### Broker Topology

> The broker topology differs from the mediator topology in that there is no central event mediator; rather, the message flow is distributed across the event processor components in a chain-like
> fashion through a lightweight message broker (e.g., ActiveMQ, HornetQ, etc.). This topology is useful when you have a relatively simple event processing flow and you do not want (or need) central
> event orchestration.
>
> There are two main types of architecture components within the broker topology: a broker component and an event processor component. The broker component can be centralized or federated and contains
> all of the event channels that are used within the event flow. The event channels contained within the broker component can be message queues, message topics, or a combination of both.
>
> Richards, Mark. "Broker Topology." *Software Architecture Patterns by Mark Richards*, O'Reilly.  
> https://www.oreilly.com/library/view/software-architecture-patterns/9781491971437/ch02.html

![](./.assets/img/broker_topology_eda.png)    
Fig. 5: Richards, Mark. "Broker Topology." *Software Architecture Patterns by Mark Richards*, O'Reilly.  
https://www.oreilly.com/library/view/software-architecture-patterns/9781491971437/ch02.html

### Choreography-based SAGA

> In a choreography-based saga, the saga participants collaborate by exchanging events. Each step of a choreography-based saga updates the database (e.g. an aggregate) and publishes a domain event.
> The first step of a saga is initiated by a command that’s invoked by an external request, such an HTTP POST. Each subsequent step is triggered by an event emitted by a previous step.
>
> Richardson, Chris. "Implementing a choreography-based saga." *Managing data consistency in a microservice architecture using Sagas*, chrisrichardson.net.  
> [https://chrisrichardson.net/post/sagas/2019/08/15/developing-sagas-part-3.html](https://chrisrichardson.net/post/sagas/2019/08/15/developing-sagas-part-3.html#:%7E:text=In%20a%20choreography%2Dbased%20saga,and%20publishes%20a%20domain%20event.&text=The%20step%20of%20the%20saga,data%20and%20emits%20an%20event)

![](./.assets/img/Saga.png)    
Fig. 6: Richardson, Chris. "Implementing a choreography-based saga." *Managing data consistency in a microservice architecture using Sagas*, chrisrichardson.net.  
[https://chrisrichardson.net/post/sagas/2019/08/15/developing-sagas-part-3.html](https://chrisrichardson.net/post/sagas/2019/08/15/developing-sagas-part-3.html#:%7E:text=In%20a%20choreography%2Dbased%20saga,and%20publishes%20a%20domain%20event.&text=The%20step%20of%20the%20saga,data%20and%20emits%20an%20event)

#### Orchestration vs Choreography

*SAGA - A long story about past events over a long period of time.*

> Orchestration entails actively controlling all elements and interactions like a conductor directs the musicians of an orchestra, while choreography entails establishing a pattern or routine that
> microservices follow as the music plays, without requiring supervision and instructions.
>
> Schabowsky, Jonathan. "The Benefits of Choreography", *solace.com*, last edited on 16 Nov 2019.   
> https://solace.com/blog/microservices-choreography-vs-orchestration/

##### Orchestration

![](./.assets/img/orchestration.png)    
Fig. 7: Falcão, Antônio. "Order orchestration-based saga".

Benefits & drawbacks of Orchestration

* Centralized logic: this can be good and bad;
* High coupling: Need to know about the capability of other services;
* Easier to understand the workflow since its defined in a central location;
* Full control over the workflow steps via commands;
* Point of failure;
* Easier to debug and test.

##### Choreography

![](./.assets/img/choreography.png)     
Fig. 8: Falcão, Antônio. "Order choreography-based saga".

Benefits & drawbacks of Choreography

* No centralized logic: this can be good and bad;
* Low coupling: Clear separation of concerns;
* Better performance: Fewer messages to handle;
* Useful for small/simple workflows;
* Difficult to conceptualize if a lot of services are involved;
* Harder to debug & test if a lot of services are involved.

## Event sourcing

> Instead of storing just the current state of the data in a domain, use an append-only store to record the full series of actions taken on that data. The store acts as the system of record and can be
> used to materialize the domain objects. This can simplify tasks in complex domains, by avoiding the need to synchronize the data model and the business domain, while improving performance,
> scalability, and responsiveness. It can also provide consistency for transactional data, and maintain full audit trails and history that can enable compensating actions.
>
> "Event Sourcing pattern" *MSDN*, Microsoft Docs, last edited on 23 Jun 2017.  
> https://docs.microsoft.com/en-us/azure/architecture/patterns/event-sourcing

> We can query an application's state to find out the current state of the world, and this answers many questions. However there are times when we don't just want to see where we are, we also want to
> know how we got there.
>
> Event Sourcing ensures that all changes to application state are stored as a sequence of events. Not just can we query these events, we can also use the event log to reconstruct past states, and as
> a foundation to automatically adjust the state to cope with retroactive changes.
>
> Fowler, Martin. "Eventsourcing", *martinfowler.com*, last edited on 12 Dec 2005.     
> https://martinfowler.com/eaaDev/EventSourcing.html

<br>

![](./.assets/img/event-sourcing-overview.png)  
Fig. 9: MSDN. *Event Sourcing pattern*.    
https://docs.microsoft.com/en-us/azure/architecture/patterns/event-sourcing#solution

### Updating entities

> To update an entity’s state we use commands from the outside and events on the inside:
>
> - Commands: The state of the entity can be changed only by sending commands to it. The commands are the "external" API of an entity. Commands request state changes. The current state may reject the
> command, or it may accept it producing zero, one or many events (depending on the command and the current state).
>
>
> - Events: The events represent changes of the entity’s state and are the only way to change it. The entity creates events from commands. Events are an internal mechanism for the entity to mutate the
> state, other parties can’t send events. Other parts of the application may listen to the created events. Summing up, events are facts new tab.
>
> The events are persisted to the datastore, while the entity state is kept in memory. In case of a restart the latest state gets rebuilt by replaying the events from the Event Journal.
>
> "Event Sourcing" *Akka platform*, developer.lightbend.com.  
> https://developer.lightbend.com/docs/akka-platform-guide/concepts/event-sourcing.html

### Pattern

> _want to learn event sourcing?_  
> _f(state, event) => state_
>
> -- <cite> gregyoung (@gregyoung) </cite>

![](./.assets/img/event-sourcing-state-transition.png)  
Fig. 10: Battermann, Leif. *12 Things You Should Know About Event Sourcing*.    
http://blog.leifbattermann.de/2017/04/21/12-things-you-should-know-about-event-sourcing/

The mantra of event sourcing and cover the four steps in slightly more details:

    1 - A command is received by an entity.
    
    2 - The entity checks to see if the command can be applied.
    
    3 - If the command can be applied:
    
        1 - The entity creates at least one event;
        2 - The entity changes state based on the event details;
        3 - The event is persisted in the store;
        4 - The event is published to the exchange.
    
    4 - If the command cannot be applied:
    
        1 - If necessary, the entity creates a failure event;
        2 - If necessary, the entity changes state based on the failure event;
        3 - The failure event is persisted in the store;
        4 - The failure event is published to the exchange.

State transition during events applying:

![](./.assets/img/applyTo_event-sourcing.png)
Fig. 11: Reitzammer, Benjamin & Seitz, Johannes. *Event Sourcingin practice*.    
https://ookami86.github.io/event-sourcing-in-practice/#making-eventsourcing-work/18-concurrent-modifictations.md

<br>

![](./.assets/img/eventsourcingrequestflow.png)
Fig. 12: Eventuate. *Event Sourcing*.    
https://eventuate.io/whyeventsourcing.html

### Event Store

> So, Event Sourcing is the persistence mechanism where each state transition for a given entity is represented as a domain event that gets persisted to an event database (event store). When the
> entity state mutates, a new event is produced and saved. When we need to restore the entity state, we read all the events for that entity and apply each event to change the state, reaching the correct
> final state of the entity when all available events are read and applied.
>
> Zimarev, Alexey. "What is Event Sourcing?", *Event Store blog*, last edited on 03 June 2020.   
> https://www.eventstore.com/blog/what-is-event-sourcing

![](./.assets/img/event-store.png)  
Fig. 13: Shilkov, Mikhail. *Event Sourcing and IO Complexity*.    
https://mikhail.io/2016/11/event-sourcing-and-io-complexity/

The following picture shows the difference between approaches:

![](./.assets/img/event-sourcing.png)
Fig. 14: Richardson, Chris. *Pattern: Event sourcing*.    
https://microservices.io/patterns/data/event-sourcing.html

### Snapshot

> Once you understand how Event Sourcing works, the most common thought is: “What happens when you have a lot of Events? Won’t it be inefficient to fetch every event from the event stream and replay
> all of them to get to the current state?”. It might be. But to combat this, you can use snapshots in event sourcing to rehydrate aggregates. Snapshots give you a representation of your aggregates
> state at a point in time. You can then use this as a checkpoint and then only replay the events since the snapshot.
>
> Comartin, Derek. "Snapshots in Event Sourcing for Rehydrating Aggregates", *codeopinion.com*, last edited on 17 Mar 2021.   
> https://codeopinion.com/snapshots-in-event-sourcing-for-rehydrating-aggregates/

> Snapshotting is an optimisation that reduces time spent on reading event from an event store. If for example a stream contains thousands of events, and we need to read all of them every time, then
> the time the system takes to handle a command will be noticeable. What we can do instead is to create a snapshot of the aggregate state and save it. Then before a command is handled we can load the
> latest snapshot and only new events since the snapshot was created.
>
> Gunia, Kacper. "Event Sourcing: Snapshotting", *domaincentric.net*, last edited on 5 Jun 2020.   
> https://domaincentric.net/blog/event-sourcing-snapshotting

![](./.assets/img/snapshot.png)  
Fig. 15: Comartin, Derek. *Snapshots in Event Sourcing for Rehydrating Aggregates*.    
https://codeopinion.com/snapshots-in-event-sourcing-for-rehydrating-aggregates/

Snapshot stream:

![](./.assets/img/stream_snapshot.png)  
Fig. 16: Comartin, Derek. *Snapshots in Event Sourcing for Rehydrating Aggregates*.    
https://codeopinion.com/snapshots-in-event-sourcing-for-rehydrating-aggregates/

## EDA + Event-sourcing

> Event sourcing a system means the treatment of events as the source of truth. In principle, until an event is made durable within the system, it cannot be processed any further. Just like an
> author’s story is not a story at all until it’s written, an event should not be projected, replayed, published or otherwise processed until it’s durable enough such as being persisted to a data store.
> Other designs where the event is secondary cannot rightfully claim to be event sourced but instead merely an event-logging system.
>
>Combining EDA with the event-sourcing pattern is another increment of the system’s design because of the alignment of the EDA principle that events are the units of change and the event-sourcing
> principle that events should be stored first and foremost.
>
> Go, Jayson. "From Monolith to Event-Driven: Finding Seams in Your Future Architecture", *InfoQ*, last edited on 15 Set 2020.   
> https://www.eventstore.com/blog/what-is-event-sourcing

Comparison overview:

| Aspects | Event sourcing            | EDA                             |
|---------|---------------------------|---------------------------------|
| Propose | Keeping history           | Highly adaptable and scalable   |
| Scope   | Single application/system | Whole organisation/several apps |
| Storage | Central event store       | Decentralised                   |
| Testing | Easier                    | Harder                          |

Table 3: Lorio, Pablo. *Comparison overview*, Event driven architectures vs event sourcing patterns.    
https://pablo-iorio.medium.com/event-driven-architectures-vs-event-sourcing-patterns-23d328289bf9

<br>

![](./.assets/img/EventSourcing_EDA.jpeg)
Fig. 17: Nowak, Aleksander. *Understanding Event-Driven Design Patterns for Microservices*.    
https://levelup.gitconnected.com/understanding-event-driven-design-patterns-for-microservices-659b3c9fb51f

## CQRS

> CQRS stands for Command and Query Responsibility Segregation, a pattern that separates read and update operations for a data store. Implementing CQRS in your application can maximize its
> performance, scalability, and security. The flexibility created by migrating to CQRS allows a system to better evolve over time and prevents update commands from causing merge conflicts at the domain
> level.
>
> Benefits of CQRS include:
>
> * **Independent scaling**. CQRS allows the read and write workloads to scale independently, and may result in fewer lock contentions.
> * **Optimized data schemas**. The read side can use a schema that is optimized for queries, while the write side uses a schema that is optimized for updates.
> * **Security**. It's easier to ensure that only the right domain entities are performing writes on the data.
> * **Separation of concerns**. Segregating the read and write sides can result in models that are more maintainable and flexible. Most of the complex business logic goes into the write model. The
> read model can be relatively simple.
> * **Simpler queries**. By storing a materialized view in the read database, the application can avoid complex joins when querying.
>
> "What is the CQRS pattern?" *MSDN*, Microsoft Docs, last edited on 2 Nov 2020.  
> https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs

![](.assets/img/cqrs.png)   
Fig. 18: Bürckel, Marco. *Some thoughts on using CQRS without Event Sourcing*.    
https://medium.com/@mbue/some-thoughts-on-using-cqrs-without-event-sourcing-938b878166a2

<br>

![](.assets/img/cqrs.jpg)   
Fig. 19: Go, Jayson. *From Monolith to Event-Driven: Finding Seams in Your Future Architecture*.    
https://www.eventstore.com/blog/what-is-event-sourcing

### Projections

To cover this topic was
prepared [this presentation](https://www.canva.com/design/DAEY9ttmPgY/F_lh7TXQEdG-su-qojEjdw/view?utm_content=DAEY9ttmPgY&utm_campaign=designshare&utm_medium=link&utm_source=publishsharelink)
with some different strategies and ways to implement projections.

![](.assets/img/projections.svg)

## Event-sourcing + CQRS

> CQRS and Event Sourcing have a symbiotic relationship. CQRS allows Event Sourcing to be used as the data storage mechanism for the domain.
>
> Young Greg, 2012, *CQRS and Event Sourcing*, **CQRS Documents by Greg Young**, p50.

> The CQRS pattern is often used along with the Event Sourcing pattern. CQRS-based systems use separate read and write data models, each tailored to relevant tasks and often located in physically
> separate stores. When used with the Event Sourcing pattern, the store of events is the write model, and is the official source of information. The read model of a CQRS-based system provides
> materialized views of the data, typically as highly denormalized views. These views are tailored to the interfaces and display requirements of the application, which helps to maximize both display and
> query performance.
>
> "Event Sourcing and CQRS pattern" *MSDN*, Microsoft Docs, last edited on 02 Nov 2020.   
> https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs#event-sourcing-and-cqrs-pattern

![](./.assets/img/cqrs-eventsourcing-diagram.png)
Fig. 20: Whittaker, Daniel. *CQRS + Event Sourcing – Step by Step*.    
https://danielwhittaker.me/2020/02/20/cqrs-step-step-guide-flow-typical-application/

<br>

![](./.assets/img/cqrs-eventsourcing-flow.png)  
Fig. 21: Katwatka, Piotr. *Event Sourcing with CQRS*.  
https://www.divante.com/blog/event-sourcing-open-loyalty-engineering

### Commands vs Events

> Events represent a past, something that already happened and can't be undone. Commands, on the other hand, represent a wish, an action in the future which can be rejected. An event has typically
> multiple consumers, but a command is addressed to only one.
>
> Tulka, Tomas. "Events vs. Commands in DDD", *blog.ttulka.com*, last edited on 25 Mar 2020.   
> https://blog.ttulka.com/events-vs-commands-in-ddd

#### Domain Event

> In domain-driven design, domain events are described as something that happens in the domain and is important to domain experts. Such events typically occur regardless of whether or to what extent
> the domain is implemented in a software system. They are also independent of technologies. Accordingly, domain events have a high-value semantics, which is expressed in the language spoken by domain
> experts.
>
> Stettler, Christina. "Domain Events vs. Event Sourcing", *innoq.com*, last edited on 15 Jan 2019.   
> https://www.innoq.com/en/blog/domain-events-versus-event-sourcing/

> Event Sourcing is when you use Domain Events to store the state of an Aggregate within a Bounded Context. This basically means replacing your relational data model (or other data store) with an
> ever-growing log of Domain Events, which is called an event store. This is the core of Event Sourcing. So to use Event Sourcing you definitely need to understand Domain Events.
>
> Holmqvist, Mattias. "What are Domain Events?", *serialized.io*, last edited on 20 Aug 2020.   
> https://serialized.io/ddd/domain-event/

#### Integration Event

> Integration events are used for bringing domain state in sync across multiple microservices or external systems. This functionality is done by publishing integration events outside the microservice.
> When an event is published to multiple receiver microservices (to as many microservices as are subscribed to the integration event), the appropriate event handler in each receiver microservice handles
> the event.
>
> MSDN. "Implementing event-based communication between microservices (integration events)", *docs.microsoft.com*, last edited on 30 Nov 2021.   
> https://docs.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/integration-event-based-microservice-communications

> Should you publish Domain Events or Integration Events? Common advice is to not publish domain events outside of your service boundary. They should only exist within your service boundary. Instead,
> you should publish integration events for other service boundaries. While this general advice makes sense, it’s not so cut-and-dry. There are many reasons why you would want to publish domain events
> for other services to consume.
>
> Domain Events or Integration Events? As always, it depends. If your domain events are stable business concepts and they are understood outside of your boundary as a part of a long-running business
> process, then yes, publishing domain events outside of your boundary are acceptable. If events are used for data propagation or are more CRUD in nature, then publish Integration Events.
>
> Comartin, Derek. "Should you publish Domain Events or Integration Events?", *codeopinion.com*, last edited on 24 Nov 2021.   
> https://codeopinion.com/should-you-publish-domain-events-or-integration-events/

##### Event Notification

> Most times events used for notifications are generally pretty slim. They don’t contain much data. If a consumer is handling an event but needs more information, to, for example, react and perform
> some action, it might have to make an RPC call back to the producing service to get more information. And this is what leads people to Event carried State Transfer, so they do not have to make these
> RPC calls.
>
> Comartin, Derek. "Event Based Architecture: What do you mean by EVENT?", *codeopinion.com*, last edited on 05 Mai 2021.   
> https://codeopinion.com/should-you-publish-domain-events-or-integration-events/

> In this mode, the event producer sends a notification to the event system that a change has happened to the entity. The change itself was NOT specified in the event. Consumers are expected to query
> the read endpoint to understand the latest state of the data.
>
> Balachandran, Arvind. "Event Notification vs. Event-Carried State Transfer", *Start it up*, last edited on 27 Oct 2019.
> https://medium.com/swlh/event-notification-vs-event-carried-state-transfer-2e4fdf8f6662

##### Event-Carried State Transfer

> The most common way I see events being used and explained is for state propagation. Meaning, you’re publishing events about state changes within a service, so other services (consumers) can keep a
> local cache copy of the data.
>
>This is often referred to as Event Carried State Transfer.
>
>The reason services will want a local cache copy of another service’s data, is so they do not need to make RPC calls to other services to get data. The issue with making the RPC call is if there are
> issues with availability or latency, the call might fail. In order to be available when other services are unavailable, they want the data they need locally.
>
> Comartin, Derek. "Event Based Architecture: What do you mean by EVENT?", *codeopinion.com*, last edited on 05 Mai 2021.   
> https://codeopinion.com/should-you-publish-domain-events-or-integration-events/

> In stark contrast to the event notification model, the event-carried state transfer model puts the data as part of the event itself. There are two key variants to implementing this. Fine-Grained and
> Snapshots.
>
> Balachandran, Arvind. "Event Notification vs. Event-Carried State Transfer", *Start it up*, last edited on 27 Oct 2019.   
> https://medium.com/swlh/event-notification-vs-event-carried-state-transfer-2e4fdf8f6662

## EventStorming

> EventStorming is a flexible workshop format for collaborative exploration of complex business domains.
>
>It comes in different flavours, that can be used in different scenarios:
>
> * to assess health of an existing line of business and to discover the most effective areas for improvements;
> * to explore the viability of a new startup business model;
> * to envision new services, that maximise positive outcomes to every party involved;
> * to design clean and maintainable Event-Driven software, to support rapidly evolving businesses.
>
> The adaptive nature of EventStorming allows sophisticated cross-discipline conversation between stakeholders with different backgrounds, delivering a new type of collaboration beyond silo and
> specialisation boundaries.
>
> Brandolini, Alberto. "EventStorming", *EventStorming.com*, last edited on 2020.   
> https://www.eventstorming.com/

![](./.assets/img/event-storming.jpg)  
Fig. 22: Baas-Schwegler, Kenny & Richardson, Chris. *Picture that explains "Almost" Everything*.    
https://github.com/ddd-crew/eventstorming-glossary-cheat-sheet

### EventStorming (WIP)

![](./.assets/img/event-storming-wip.jpg)

### From EventStorming to Event Sourcing

![](./.assets/img/from-eventstorming-to-eventsourcing.png)

## Domain-driven design (DDD)

> Domain-Driven Design is an approach to software development that centers the development on programming a domain model that has a rich understanding of the processes and rules of a domain. The name
> comes from a 2003 book by Eric Evans that describes the approach through a catalog of patterns. Since then a community of practitioners have further developed the ideas, spawning various other books
> and training courses. The approach is particularly suited to complex domains, where a lot of often-messy logic needs to be organized.
>
> Fowler, Martin. "DomainDrivenDesign", *martinfowler.com*, last edited on 22 April 2020.  
> https://martinfowler.com/bliki/DomainDrivenDesign.html

### Aggregate

> I think a model is a set of related concepts that can be applied to solve a problem.    
> -- <cite> Eric Evans </cite>

![](./.assets/img/aggregate.png)  
Fig. 23: Vernon, V. (2016), Aggregates from Domain-Driven Design Distilled, 1st ed, p78.

> Each Aggregate forms a transactional consistency boundary. This means that within a single Aggregate, all composed parts must be consistent, according to business rules, when the controlling
> transaction is committed to the database. This doesn't necessarily mean that you are not supposed to compose other elements within an Aggregate that don't need to be consistent after a transaction.
> After all, an Aggregate also models a conceptual whole. But you should be first and foremost concerned with transactional consistency. The outer boundary drawn around Aggregate Type 1 and Aggregate
> Type 2 represents a separate transaction that will be in control of atomically persisting each object cluster.
>
> Vernon, V. (2016) Domain-Driven Design Distilled, 1st ed. New York: Addison-Wesley Professional, p78.

> Aggregate is a pattern in Domain-Driven Design. A DDD aggregate is a cluster of domain objects that can be treated as a single unit. An example may be an order and its line-items, these will be
> separate objects, but it's useful to treat the order (together with its line items) as a single aggregate.
>
> Fowler, Martin. "DDD_Aggregate", *martinfowler.com*, last edited on 08 Jun 2015.  
> https://martinfowler.com/bliki/DomainDrivenDesign.html

### Bounded Context

> Basically, the idea behind bounded context is to put a clear delineation between one model and another model. This delineation and boundary that's put around a domain model, makes the model that is
> inside the boundary very explicit with very clear meaning as to the concepts, the elements of the model, and the way that the team, including domain experts, think about the model.
>
> You'll find a ubiquitous language that is spoken by the team and that is modeled in software by the team. In scenarios and discussions where somebody says, for example, "product," they know in that
> context exactly what product means. In another context, product can have a different meaning, one that was defined by another team. The product may share identities across bounded contexts, but,
> generally speaking, the product in another context has at least a slightly different meaning, and possibly even a vastly different meaning.
>
> Vernon, Vaughn. "Modeling Uncertainty with Reactive DDD", *www.infoq.com*, last edited on 29 Set 2018.  
> https://martinfowler.com/bliki/BoundedContext.html

![](./.assets/img/BoundedContext.jpg)  
Fig. 24: Martin, Fowler. *BoundedContext*.    
https://martinfowler.com/bliki/BoundedContext.html

> First, a Bounded Context is a semantic contextual boundary. This means that within the boundary each component of the software model has a specific meaning and does specific things. The components
> inside a Bounded Context are context specific and semantically motivated. That’s simple enough.
>
> When you are just getting started in your software modeling efforts, your Bounded Context is somewhat conceptual. You could think of it as part of your problem space. However, as your model starts
> to take on deeper meaning and clarity, your Bounded Context will quickly transition to your solution space , with your software model being reflected as project source code. Remember that a Bounded
> Context is where a model is implemented, and you will have separate software artifacts for each Bounded Context.
>
> Vernon, V. (2016). "Strategic Design with Bounded Contexts and the Ubiquitous Language", Domain-Driven Design Distilled, 1st ed. New York: Addison-Wesley Professional.

> Explicitly define the context within which a model applies. Explicitly set boundaries in terms of team organization, usage within specific parts of the application, and physical manifestations such
> as code bases and database schemas. Apply Continuous Integration to keep model concepts and terms strictly consistent within these bounds, but don’t be distracted or confused by issues outside.
> Standardize a single development process within the context, which need not be used elsewhere.
>
> Evans, Eric. (2015). "Bounded Context", Domain-Driven Design Reference.   
> https://www.domainlanguage.com/ddd/reference/

## Clean Architecture

> Clean architecture is a software design philosophy that separates the elements of a design into ring levels. An important goal of clean architecture is to provide developers with a way to organize
> code in such a way that it encapsulates the business logic but keeps it separate from the delivery mechanism.
>
> The main rule of clean architecture is that code dependencies can only move from the outer levels inward. Code on the inner layers can have no knowledge of functions on the outer layers. The
> variables, functions and classes (any entities) that exist in the outer layers can not be mentioned in the more inward levels. It is recommended that data formats also stay separate between levels.
>
> "Clean Architecture." *Whatis*, last edited on 10 Mar 2019.  
> https://whatis.techtarget.com/definition/clean-architecture

![](./.assets/img/CleanArchitecture.jpg)  
Fig. 25: C. Martin, Robert. *The Clean Architecture*.    
https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html

## Performance

### Minimize exceptions

> Exceptions should be rare. Throwing and catching exceptions is slow relative to other code flow patterns. Because of this, exceptions shouldn't be used to control normal program flow.
>
> Recommendations:
>
> - Do not use throwing or catching exceptions as a means of normal program flow, especially in hot code paths.
> - Do include logic in the app to detect and handle conditions that would cause an exception.
> - Do throw or catch exceptions for unusual or unexpected conditions.
> - App diagnostic tools, such as Application Insights, can help to identify common exceptions in an app that may affect performance.
>
> "ASP.NET Core Performance Best Practices" *MSDN*, Microsoft Docs, last edited on 18 Fev 2022.  
> https://docs.microsoft.com/en-us/aspnet/core/performance/performance-best-practices?view=aspnetcore-6.0#minimize-exceptions

### Pool HTTP connections with HttpClientFactory

> Closed `HttpClient` instances leave sockets open in the `TIME_WAIT` state for a short period of time. If a code path that creates and disposes of `HttpClient` objects is frequently used, the app may
> exhaust available sockets.
>
> Recommendations:
>
> - Do not create and dispose of HttpClient instances directly.
> - Do use HttpClientFactory to retrieve HttpClient instances.
>
> "ASP.NET Core Performance Best Practices" *MSDN*, Microsoft Docs, last edited on 18 Fev 2022.  
> https://docs.microsoft.com/en-us/aspnet/core/performance/performance-best-practices?view=aspnetcore-6.0#pool-http-connections-with-httpclientfactory

### DbContext pooling

> The basic pattern for using EF Core in an ASP.NET Core application usually involves registering a custom DbContext type into the dependency injection system and later obtaining instances of that
> type through constructor parameters in controllers. This means a new instance of the DbContext is created for each request.
>
> In version 2.0 we are introducing a new way to register custom DbContext types in dependency injection which transparently introduces a pool of reusable DbContext instances. This is conceptually
> similar to how connection pooling operates in ADO.NET providers and has the advantage of saving some of the cost of initialization of DbContext instance.
>
> "New features in EF Core 2.0" *MSDN*, Microsoft Docs, last edited on 11 Oct 2020.  
> https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-2.0/#dbcontext-pooling

### Snapshotting

> Snapshotting is an optimisation that reduces time spent on reading event from an event store.
>
> Gunia, Kacper. "Event Sourcing: Snapshotting", *domaincentric.net*, last edited on 5 Jun 2020.   
> https://domaincentric.net/blog/event-sourcing-snapshotting

More details in [snapshot](#snapshot) section.

## Running

### Development

Projects may have different environments where the application runs: development, staging, production, etc. Usually, different environments should have different secrets.

Secret Manager allows each developer to configure the desired infrastructure just once, avoiding changing every new branch besides avoiding storing passwords or other sensitive data in source code or
local configuration files.

#### User-secrets

Define app secrets for **each project**. The secret is associated with the project's `UserSecretsId` value. The command must be run from the directory in which the project file exists:

```bash
dotnet user-secrets set "ConnectionStrings:EventStore" "Server=<IP_ADDRESS>,1433;Database=<YourContextName>EventStore;User=sa;Password=<PASSWORD>;trustServerCertificate=true"
dotnet user-secrets set "ConnectionStrings:Projections" "mongodb://<USER_NAME>:<PASSWORD>@<IP_ADDRESS>:27017/<YourContextName>Projections/?authSource=admin"
dotnet user-secrets set "QuartzOptions:quartz.dataSource.default.connectionString" "Server=<IP_ADDRESS>,1433;Database=Quartz;User=sa;Password=<PASSWORD>;trustServerCertificate=true"
```

Expected:

```bash
dotnet user-secrets list
ConnectionStrings:EventStore = Server=<IP_ADDRESS>,1433;Database=<YourContextName>EventStore;User=sa;Password=!MyStrongPassword;trustServerCertificate=true
ConnectionStrings:Projections = mongodb://<USER_NAME>:<PASSWORD>@<IP_ADDRESS>:27017/<YourContextName>Projections/?authSource=admin
QuartzOptions:quartz.dataSource.default.connectionString = Server=<IP_ADDRESS>,1433;Database=Quartz;User=sa;Password=!MyStrongPassword;trustServerCertificate=true
```

A batch of secrets can be set by piping JSON to the set command, as in the following example:

```json
{
  "QuartzOptions:quartz.dataSource.default.connectionString": "Server=127.0.0.1,1433;Database=Quartz;User=sa;Password=!MyStrongPassword;trustServerCertificate=true",
  "ConnectionStrings:Projections": "mongodb://mongoadmin:secret@127.0.0.1:27017/ShoppingCartProjections/?authSource=admin",
  "ConnectionStrings:EventStore": "Server=127.0.0.1,1433;Database=ShoppingCartEventStore;User=sa;Password=!MyStrongPassword;trustServerCertificate=true"
}
```

_Windows_

```shell
type .\input.json | dotnet user-secrets set
```

_Linux/Mac_

```bash
cat ./input.json | dotnet user-secrets set
```

The values are stored in a JSON file in the local machine's user profile folder:

_Windows_

```bash
%APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json
```

_Linux/Mac_

```bash
~/.microsoft/usersecrets/<user_secrets_id>/secrets.json
```

#### Docker

The respective [./docker-compose.Development.yaml](./docker-compose.Development.yaml) will provide all the necessary resources (with proper exposure of the connection ports) for a development
environment:

```bash
docker-compose -f ./docker-compose.Development.yaml up -d
```

If prefer, is possible to use individual Docker commands:

MSSQL

```bash
docker run -d \
-e 'ACCEPT_EULA=Y' \
-e 'SA_PASSWORD=!MyStrongPassword' \
-p 1433:1433 \
--name mssql \
mcr.microsoft.com/mssql/server
```

MongoDB

```bash
docker run -d \
-e 'MONGO_INITDB_ROOT_USERNAME=mongoadmin' \
-e 'MONGO_INITDB_ROOT_PASSWORD=secret' \
-p 27017:27017 \
--name mongodb \
mongo
```

RabbitMQ

```bash
docker run -d \
-p 15672:15672 \
-p 5672:5672 \
--hostname my-rabbit \
--name rabbitmq \
rabbitmq:3-management
```

### Staging

An environment for testing that exactly resembles the production environment. In other words, it's a complete but independent copy of the production environment, including the database.

To preserve this concept, and considering a containerized deployment strategy (immutable environment), the staging environment is provided via [./docker-compose.yaml](./docker-compose.yaml)
accordingly with each `appsettings.Staging.json`, considering Docker has a network interface with an IP address, a gateway, a routing table, DNS services, and other networking details.

#### Docker

The respective [./docker-compose.yaml](./docker-compose.Development.yaml) will provision all system dependencies, with minimal resources needed, and expose only the **WebAPP** connection port (as in
Production environment):

```bash
docker-compose up -d
```

Worker Services

```yaml
deploy:
  replicas: 2
  resources:
    limits:
      cpus: '0.20'
      memory: 120M
```

Web API

```yaml
deploy:
  replicas: 2
  resources:
    limits:
      cpus: '0.20'
      memory: 120M
```

## Test

K6

```bash
docker run --network=ecommerce --name k6 --rm -i grafana/k6 run - <test.js
```

## Event store

### Store event

```sql
CREATE TABLE [ShoppingCartStoreEvents] (
    [Version] int NOT NULL IDENTITY,
    [AggregateId] uniqueidentifier NOT NULL,
    [AggregateName] varchar(30) NOT NULL,
    [EventName] varchar(50) NOT NULL,
    [Event] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ShoppingCartStoreEvents] PRIMARY KEY ([Version])
);
```

![](./.assets/img/store-event.png)

```json
{
  "$type": "ECommerce.Contracts.ShoppingCart.DomainEvents+CartCreated, ECommerce",
  "CartId": "f1c2ff9b-d8a5-43ce-9c3e-684364398bf0",
  "CustomerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "Timestamp": "2022-02-23T15:49:22.2555835-03:00",
  "CorrelationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

### Snapshot

```sql
CREATE TABLE [ShoppingCartSnapshots] (
    [AggregateVersion] int NOT NULL,
    [AggregateId] uniqueidentifier NOT NULL,
    [AggregateName] varchar(30) NOT NULL,
    [AggregateState] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ShoppingCartSnapshots] PRIMARY KEY ([AggregateVersion], [AggregateId])
);
```

![](./.assets/img/store-snapshot.png)

```json
{
  "CustomerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "Status": 2,
  "ShippingAddress": null,
  "BillingAddress": null,
  "Total": 0.0,
  "Items": [],
  "PaymentMethods": [],
  "Id": "b7bc74a0-c5ad-4510-baaa-8b5cbfc1cea3",
  "IsDeleted": false
}
```

### Migrations

```bash
dotnet ef migrations add "First Migration" -s .\WorkerService\ -p .\Infrastructure.EventStore\
```

## Main references:

* [Evans, Eric (2003), Domain-Driven Design: Tackling Complexity in the Heart of Software.](https://www.amazon.com/dp-0321125215/dp/0321125215/ref=mt_other?_encoding=UTF8&me=&qid=1641385448)
* [Hohpe, Gregor (2003), Enterprise Integration Patterns: Designing, Building, and Deploying Messaging Solutions](https://www.enterpriseintegrationpatterns.com/)
* [Young, Greg (2012), Event Centric: Finding Simplicity in Complex Systems.](https://www.amazon.com/Event-Centric-Simplicity-Addison-Wesley-Signature/dp/0321768221)
* [Vernon, Vaughn (2016), Domain-Driven Design Distilled.](https://www.amazon.com/dp-0134434420/dp/0134434420/ref=mt_other?_encoding=UTF8&me=&qid=1641385096)
* [Richardson, Chris (2018), Microservices Patterns: With examples in Java.](https://www.amazon.com/-/pt/dp-B09782192F/dp/B09782192F/ref=mt_other?_encoding=UTF8&me=&qid=1641385683)

## Complementary references

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
* [Event Sourcing: Aggregates Vs Projections - Kacper Gunia](https://domaincentric.net/blog/event-sourcing-aggregates-vs-projections)
* [Event Sourcing: Projections - Kacper Gunia](https://domaincentric.net/blog/event-sourcing-projections)
* [Advantages of the event-driven architecture pattern - Grace Jansen & Johanna Saladas](https://developer.ibm.com/articles/advantages-of-an-event-driven-architecture/)

## Built With

### Worker Services

* [.NET 7 preview](https://dotnet.microsoft.com/en-us/) - A free, multi/cross-platform and open-source framework;
* [EF Core 7 preview](https://devblogs.microsoft.com/dotnet/announcing-entity-framework-7-preview-1/) - An open source object–relational mapping framework for ADO.NET;
* [MSSQL](https://hub.docker.com/_/microsoft-mssql-server) - A relational database management system (Event Store Database);
* [MongoDB](https://www.mongodb.com/docs/drivers/csharp/) - A source-available cross-platform document-oriented database (Projections Database);
* [MassTransit](https://masstransit-project.com/) - Message Bus;
* [FluentValidation](https://fluentvalidation.net/) - A popular .NET library for building strongly-typed validation rules;
* [Serilog](https://serilog.net/) - A diagnostic logging to files, console and elsewhere.

### Web API

* [ASP.NET Core 7](https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-net-7-preview-1/) - A free, cross-platform and open-source web-development framework;
* [MassTransit](https://masstransit-project.com/) - Message Bus;
* [FluentValidation](https://fluentvalidation.net/) - A popular .NET library for building strongly-typed validation rules;
* [Serilog](https://serilog.net/) - A diagnostic logging to files, console and elsewhere.

### Web APP

* [Blazor WASM](https://docs.microsoft.com/en-us/aspnet/core/blazor/?WT.mc_id=dotnet-35129-website&view=aspnetcore-6.0#blazor-webassembly) - Is a single-page app (SPA) framework for building
  interactive client-side web apps with .NET;
* [BlazorStrap](https://blazorstrap.io/V5/) - Bootstrap 5 Components for Blazor Framework;

## Contributing

All contributions are welcome. Please take a look at [contributing](./CONTRIBUTING.md) guide.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/tags).

## Authors

> See the list of [contributors](https://github.com/AntonioFalcao/EDA.CleanArch.DDD.CQRS.EventSourcing/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE](./LICENSE) file for details