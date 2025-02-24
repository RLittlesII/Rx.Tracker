# Mediator

## Design Decision

This Mediator Pattern implementation was patterned after a CQRS concept. The thought behind it is you have Queries that provide data, Commands that store data,
and Notifications when concerns happen. It is likely more ambitious of an interface than what most would use.

## Implementation Options

- [ ] Implement our own Mediator
- [ ] Facade
    - [ ] MediatR
        - Works with `IServiceProvider`
        - Familiar
        - DI Registration Magics
    - [ ] Shiny.Mediator
        - Attribute based handlers
        - Handlers are geared towards mobile
        - DI is auto generated for `IServiceCollection`