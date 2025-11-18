# AgileBoard

![CI - Build and Test](https://github.com/fszymaniak/AgileBoard/workflows/CI%20-%20Build%20and%20Test/badge.svg)

A Domain-Driven Design (DDD) application following Clean Architecture principles for managing agile project epics.

## Architecture

This project implements Clean Architecture with DDD patterns:
- **Core Layer**: Domain entities, value objects, domain services, and policies
- **Application Layer**: Use cases, commands, and application services
- **Infrastructure Layer**: Data access, repositories, and external integrations
- **API Layer**: REST API controllers and endpoints

## Testing

The project includes comprehensive testing:
- **Unit Tests**: Testing value objects, entities, and services
- **Architectural Tests**: Automated enforcement of Clean Architecture and DDD principles
  - Layer dependency validation
  - Naming convention enforcement
  - Domain-driven design pattern validation

Run tests locally:
```bash
# Run all tests
dotnet test

# Run only architectural tests
dotnet test --filter "FullyQualifiedName~Architecture"
```

## Architectural Tests

Architectural tests automatically verify:
- Core layer has no dependencies on outer layers
- Application layer doesn't depend on Infrastructure or API
- Proper naming conventions (Controllers, Services, Repositories, etc.)
- Value Objects are immutable and sealed
- Policies implement proper interfaces
- Commands are immutable records
- Controllers communicate through Application layer abstraction