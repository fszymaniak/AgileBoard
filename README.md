# AgileBoard

![CI - Build and Test](https://github.com/fszymaniak/AgileBoard/workflows/CI%20-%20Build%20and%20Test/badge.svg)

A Domain-Driven Design (DDD) application following Clean Architecture principles for managing agile project epics.

## Architecture

This project implements Clean Architecture with DDD patterns:
- **Core Layer**: Domain entities, value objects, domain services, and policies
- **Application Layer**: Use cases, commands, and application services
- **Infrastructure Layer**: Data access, repositories, and external integrations
- **API Layer**: REST API controllers and endpoints

## Features

- ✅ **Clean Architecture** with strict layer separation
- ✅ **Domain-Driven Design** patterns (entities, value objects, aggregates)
- ✅ **JWT Authentication** for secure API access
- ✅ **Role-Based Authorization** via policy pattern
- ✅ **Structured Logging** with Serilog
- ✅ **Nullable Reference Types** enabled for null-safety
- ✅ **PostgreSQL** database with Entity Framework Core
- ✅ **Comprehensive Testing** (unit + architectural tests)

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

## Configuration

### Environment Variables

Create a `.env` file for local development (see `.env.example`):
```bash
POSTGRES_USER=agileuser
POSTGRES_PASSWORD=your_secure_password
POSTGRES_DB=agileboard
```

### JWT Authentication

Configure JWT settings in `appsettings.json` or via environment variables:
```json
{
  "Jwt": {
    "Issuer": "AgileBoard.Api",
    "Audience": "AgileBoard.Client",
    "SecretKey": "your-256-bit-secret-key-here",
    "ExpirationMinutes": 60
  }
}
```

**Important**: Never commit secrets to version control. Use environment variables or Azure Key Vault in production.

## Running the Application

### With Docker Compose

```bash
# Start PostgreSQL
docker-compose up -d

# Run the API
dotnet run --project src/AgileBoard.Api
```

### Database Migrations

```bash
# Apply migrations
dotnet ef database update --project src/AgileBoard.Infrastructure --startup-project src/AgileBoard.Api
```

## Security

- ✅ JWT Bearer authentication required for all endpoints
- ✅ Role-based authorization via JobTitle claims
- ✅ Secure PostgreSQL credentials (no trust auth)
- ✅ HTTPS recommended for production
- ✅ Structured logging for audit trails

## Logging

Application uses Serilog for structured logging:
- **Console**: Development output
- **File**: Rolling log files in `logs/` directory
- **Structured**: JSON-formatted logs for analysis

View logs:
```bash
tail -f logs/agileboard-*.log
```