# LogiSphere

> **A Distributed, Multi-Tenant Logistics & Fleet Optimization SaaS Platform**

LogiSphere is a backend SaaS platform built with **.NET / C#** that enables logistics companies to manage fleet operations, shipments, and tenants at scale. The project follows **Clean Architecture** principles with a clear separation of concerns across Domain, Application, Infrastructure, and Host layers.

---

## Architecture Overview

The solution is structured around **Clean Architecture** (also known as Onion Architecture), ensuring that core business logic remains independent of external frameworks and infrastructure concerns.

```
┌─────────────────────────────────────────────┐
│              LogiSphere.Host                │  ← Entry point (ASP.NET Core Web API)
│         Controllers · Middleware · DI       │
└──────────────────┬──────────────────────────┘
                   │
┌──────────────────▼──────────────────────────┐
│           LogiSphere.Application            │  ← Use cases, CQRS, business orchestration
│    Commands · Queries · DTOs · Validators   │
└──────────────────┬──────────────────────────┘
                   │
┌──────────────────▼──────────────────────────┐
│             LogiSphere.Domain               │  ← Core business logic (no external deps)
│      Entities · Aggregates · Interfaces     │
└─────────────────────────────────────────────┘
                   ▲
┌──────────────────┴──────────────────────────┐
│          LogiSphere.Infrastructure          │  ← EF Core, DB, external services
│     Repositories · DbContext · Auth · Cache │
└─────────────────────────────────────────────┘
```

Dependencies always point **inward**. The Domain layer has no knowledge of any outer layer.

---

## Project Structure

```
LogiSphere/
├── LogiSphere.Domain/          # Enterprise business rules & domain entities
├── LogiSphere.Application/     # Application use cases (CQRS, services, DTOs)
├── LogiSphere.Infrastructure/  # Data access, EF Core, external integrations
├── LogiSphere.Host/            # ASP.NET Core Web API host & configuration
└── LogiSphere.slnx             # Solution file
```

### Layer Responsibilities

**`LogiSphere.Domain`**

- Entities, Aggregates, and Value Objects
- Domain interfaces (repository contracts, domain services)
- Domain events and business rule enforcement
- No dependency on any framework or external library
  **`LogiSphere.Application`**
- CQRS pattern implementation (Commands & Queries via MediatR)
- Application service interfaces
- DTOs / request-response models
- Input validation (FluentValidation)
- Pipeline behaviors (logging, validation, error handling)
  **`LogiSphere.Infrastructure`**
- EF Core `DbContext` implementation
- Repository implementations
- JWT authentication & authorization
- Caching strategies
- External service integrations
  **`LogiSphere.Host`**
- ASP.NET Core Web API controllers
- Middleware pipeline configuration
- Dependency injection registration
- Application startup and configuration

---

## Key Features

- **Multi-Tenancy** — Isolated tenant data with shared infrastructure
- **Fleet Management** — Track and optimize vehicle and driver assignments
- **CQRS Pattern** — Separate read and write models for scalability
- **Clean Architecture** — Testable, maintainable, and framework-independent core
- **JWT Authentication** — Secure token-based access control
- **EF Core with Code-First Migrations** — Structured and version-controlled data access
- **Middleware Pipeline** — Custom request handling and cross-cutting concerns

---

## Tech Stack

| Layer           | Technology               |
| --------------- | ------------------------ |
| Language        | C# (.NET)                |
| Web Framework   | ASP.NET Core Web API     |
| ORM             | Entity Framework Core    |
| CQRS / Mediator | MediatR                  |
| Authentication  | JWT Bearer               |
| Validation      | FluentValidation         |
| Architecture    | Clean Architecture / DDD |

---

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or later)
- SQL Server or PostgreSQL (configured via connection string)
- Git

### Clone the Repository

```bash
git clone https://github.com/mahbub47/LogiSphere.git
cd LogiSphere
```

### Configure the Application

Update the connection string and JWT settings in `LogiSphere.Host/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "your-database-connection-string"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key",
    "Issuer": "LogiSphere",
    "Audience": "LogiSphereClients",
    "ExpiryMinutes": 60
  }
}
```

### Apply Migrations

```bash
dotnet ef database update --project LogiSphere.Infrastructure --startup-project LogiSphere.Host
```

### Run the Application

```bash
dotnet run --project LogiSphere.Host
```

The API will be available at `https://localhost:5001` (or the configured port).

---

## Design Principles

This project demonstrates several backend engineering best practices:

- **SOLID Principles** — Single responsibility, open/closed design across all layers
- **Domain-Driven Design (DDD)** — Rich domain model with bounded contexts
- **Repository Pattern** — Abstractions in Domain, implementations in Infrastructure
- **Dependency Inversion** — All dependencies point toward the Domain layer
- **Separation of Concerns** — Each layer has a single, well-defined responsibility

---

## Contributing

Contributions, issues, and feature requests are welcome. Feel free to open an issue or submit a pull request.

---

## License

This project is open source and available under the [MIT License](LICENSE).
