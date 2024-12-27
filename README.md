# Rx Tracker
This is an application to schedule, track, and report taken medication.  The main purpose of this application is to allow a user to schedule a given medication, receive reminders, and explicitly acknowledge the time it was taken.

## Features
- Main Page
  - [ ] Display Calendar for current day
  - [ ] Display week view at the top
  - [ ] Highlight Events
    - Within 2 hours => Yellow
    - Within 1 hour => Orange
    - Within 30 minutes => Red
  - [ ] Count Down Timer to next medicine?
  - Tap Cards to Edit
  - Tap to add medicine

- [Add Medication](src/Rx.Tracker/Features/Medications/README.md)

## Architecture
- [Mediator](src/Rx.Tracker/Mediation/README.md)
- [Navigation](src/Rx.Tracker/Navigation/README.md)
- [State](src/Rx.Tracker/State/README.md)


### OSS Projects
- DryIoc
- Prism
- Stateless
- Serilog
- Shiny
- ReactiveUI
- ReactiveMarbles