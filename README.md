Developed by me, assisted by Claude Code

# Reservations API

A RESTful API for managing service appointments, built with Clean Architecture and CQRS pattern.

## About

This project is a portfolio application that demonstrates professional .NET development practices. It allows clients to book appointments with service providers, with full lifecycle management — scheduling, confirmation, cancellation, and rescheduling.

## Architecture

The solution follows **Clean Architecture** with four layers:

```
Reservations.Domain          ← Entities, enums, domain exceptions
Reservations.Application     ← CQRS commands/queries, interfaces, validators
Reservations.Infrastructure  ← EF Core, repositories, JWT service
Reservations.API             ← Controllers, middleware, DI configuration
```

Dependencies point inward: Infrastructure and API depend on Application, which depends only on Domain.

**CQRS** is implemented via MediatR — write operations (Commands) and read operations (Queries) are handled by separate classes, with a `ValidationBehavior` pipeline interceptor that runs FluentValidation before every handler.

## Tech Stack

| Technology | Purpose |
|---|---|
| .NET 10 | Framework |
| ASP.NET Core | Web API |
| MediatR 14 | CQRS / Mediator pattern |
| Entity Framework Core 10 | ORM |
| PostgreSQL | Database |
| Npgsql | EF Core PostgreSQL provider |
| FluentValidation | Input validation |
| JWT Bearer | Authentication |
| Swagger / Swashbuckle | API documentation |

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) (local instance)
- `dotnet-ef` tool: `dotnet tool install --global dotnet-ef`

## Getting Started

**1. Clone the repository**
```bash
git clone <repository-url>
cd ProjectNETCQRS
```

**2. Configure the database and JWT settings**

Edit `src/Reservations.API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=reservations_db;Username=postgres;Password=your_password"
  },
  "Jwt": {
    "SecretKey": "your-secret-key-minimum-32-characters",
    "Issuer": "Reservations.API",
    "Audience": "Reservations.Client"
  }
}
```

**3. Apply database migrations**
```bash
dotnet ef database update --project src/Reservations.Infrastructure --startup-project src/Reservations.API
```

**4. Run the application**
```bash
dotnet run --project src/Reservations.API
```

**5. Open Swagger UI**
```
http://localhost:5289/swagger
```

## API Endpoints

### Providers
| Method | Route | Description | Auth |
|---|---|---|---|
| POST | `/api/providers` | Register a new provider | No |
| POST | `/api/providers/login` | Authenticate and receive JWT token | No |

### Clients
| Method | Route | Description | Auth |
|---|---|---|---|
| POST | `/api/clients` | Register a new client | No |

### Appointments
| Method | Route | Description | Auth |
|---|---|---|---|
| POST | `/api/appointments` | Create an appointment | Yes |
| GET | `/api/appointments/{id}` | Get appointment by ID | Yes |
| GET | `/api/appointments?clientId=&providerId=` | List appointments by filter | Yes |
| PUT | `/api/appointments/{id}/cancel` | Cancel an appointment | Yes |
| PUT | `/api/appointments/{id}/reschedule` | Reschedule an appointment | Yes |

## Authentication

The API uses JWT Bearer authentication. To access protected endpoints:

1. Register a provider: `POST /api/providers`
2. Login to receive a token: `POST /api/providers/login`
3. Include the token in requests:
   ```
   Authorization: Bearer <your-token>
   ```

In Swagger UI, click the **Authorize** button and paste the token directly (without the `Bearer` prefix).

## Project Structure

```
src/
├── Reservations.Domain/
│   ├── Common/          ← Base Entity class
│   ├── Entities/        ← Appointment, Client, Provider, ServiceType
│   ├── Enums/           ← AppointmentStatus
│   └── Exceptions/      ← DomainException
│
├── Reservations.Application/
│   ├── Appointments/
│   │   ├── Commands/    ← CreateAppointment, CancelAppointment, RescheduleAppointment
│   │   └── Queries/     ← GetAppointmentById, ListAppointments
│   ├── Clients/
│   │   └── Commands/    ← RegisterClient
│   ├── Providers/
│   │   └── Commands/    ← RegisterProvider
│   └── Common/
│       ├── Behaviors/   ← ValidationBehavior (MediatR pipeline)
│       └── Interfaces/  ← Repository and service contracts
│
├── Reservations.Infrastructure/
│   ├── Authentication/  ← JwtService
│   └── Persistence/
│       ├── Configurations/  ← EF Core Fluent API mappings
│       ├── Repositories/    ← EF Core repository implementations
│       └── AppDbContext.cs
│
└── Reservations.API/
    ├── Controllers/     ← AppointmentsController, ClientsController, ProvidersController
    ├── Middleware/      ← ErrorHandlingMiddleware
    └── Program.cs       ← DI registration and pipeline configuration
```

## Domain Rules

- An appointment cannot be scheduled if the provider already has a confirmed or scheduled appointment in the same time slot.
- Status transitions are enforced by the domain: only valid transitions are allowed (e.g., a completed appointment cannot be canceled).
- Provider and client emails must be unique.
- Cascade deletes are disabled — appointments are protected by `ON DELETE RESTRICT` on all foreign keys.
