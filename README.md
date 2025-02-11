# Rx Tracker

This is an application to schedule, track, and report taken medication. The main purpose of this application is to allow a user to schedule a given medication,
receive reminders, and explicitly acknowledge the time it was taken.

### Builds

| Build        | Status                                                                                                                                                                                |
|--------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Pull Request | [![pull-request](https://github.com/RLittlesII/Rx.Tracker/actions/workflows/pull-request.yml/badge.svg)](https://github.com/RLittlesII/Rx.Tracker/actions/workflows/pull-request.yml) |
| Integration  | [![integration](https://github.com/RLittlesII/Rx.Tracker/actions/workflows/integration.yml/badge.svg)](https://github.com/RLittlesII/Rx.Tracker/actions/workflows/integration.yml)    |

## Features

- [Medications](src/Rx.Tracker/Features/Medications/README.md)
- [Schedule](src/Rx.Tracker/Features/Schedule/README.md)

## Architecture

- [Mediator](src/Rx.Tracker/Mediation/README.md)
- [Navigation](src/Rx.Tracker/Navigation/README.md)
- [State](src/Rx.Tracker/State/README.md)

### Future Enhancements

- [ ] State Machine should own its own configuration
    - Likely take providing delegates
- [ ] State's and Triggers should be standardized where they make sense
    - Do not force square pegs in round holes!
- [ ] Tests that can test individual guards, by using Stateless ability define guards as functions
- [ ] Pass Parameters to initialization
- [ ] Snap Shot Testing State Transitions
- [ ] States mapping to Control Template or Visual State Manager

### OSS Projects

- [DryIoc](https://github.com/dadhi/DryIoc)
- [FluentAssertions](https://github.com/fluentassertions/fluentassertions)
- [Language Extensions](https://github.com/louthy/language-ext)
- [mapperly](https://github.com/riok/mapperly)
- [MediatR](https://github.com/jbogard/MediatR)
- [Nanoid](https://github.com/codeyu/nanoid-net)
- [Noda Time](https://github.com/nodatime/nodatime)
- [NSubstitute](https://github.com/nsubstitute/NSubstitute)
- [Plugin.Maui.Calendar](https://github.com/yurkinh/Plugin.Maui.Calendar)
- [Plugin.Maui.CalendarStore](https://github.com/jfversluis/Plugin.Maui.CalendarStore)
- [Prism](https://github.com/PrismLibrary/Prism)
- [Stateless](https://github.com/dotnet-state-machine/stateless)
- [Serilog](https://github.com/serilog/serilog)
- [Shiny](https://github.com/shinyorg/shiny)
- [ReactiveUI](https://github.com/reactiveui/ReactiveUI)
- [Reactive Marbles](https://github.com/reactivemarbles)
- [xUnit](https://github.com/xunit/xunit)


