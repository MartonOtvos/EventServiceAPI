# Event Service API (.NET + PostgreSQL)
[![CI](https://github.com/MartonOtvos/EventServiceAPI/actions/workflows/ci.yml/badge.svg)](...)

A minimal event ingestion and retrieval service built with ASP.NET Core and PostgreSQL.

---

## Features

- REST API fir event ingestion and retrieval
- PostgreSQL persistence
- Asynchronous event processing via background worker
- Dockerized setup with Docker Compose
- Simple schema initialization on startup

---

## Tech Stack

- .NET (ASP.NET Core Web API)
- C#
- PostgreSQL
- Npgsql
- Docker & Docker Compose

---

## Getting Started

### Prerequisites

- Docker
- Docker Compose

### Run the application

```bash
docker compose up --build
```

#### API will be available at:

```bash
http://localhost:5000
```

#### Swagger documentation will be available at:

```bash
http://localhost:5000/swagger
```

### API Endpoints

#### Create Event

```bash
POST /events
Content-Type: application/json
```
Example request

```bash
{
    "type": "user_created",
    "source": "web"
}
```

#### Get All Events

```bash
GET /events
```

Response

```bash
[
    {
        "type": "user_created",
        "source": "web"
    },
    {
        "type": "browser_generated",
        "source": "web"
    }
]
```

#### Health check

```bash
GET /health
```

## Running Tests

The project includes minimal integration tests for the POST endpoint using xUnit

Run tests locally:

```bash
dotnet test
```

## Testing approach

The tests are designed to validate API behaviour rather than internal implementation

- Uses in-memory fallback instead of DB connection
- Focuses on request/response validation
- Avoids external dependencies for fast and reliable execution

---

## Continuous Integration

A GitHub Actions workflow is configured to automatically build and test the project on every push and pull request

---

## Database

- PostgreSQL running in docker container
- Schema is initialized automatically on application startup

---

## Design Notes

- Uses async/await for non-blocking IO
- In-memory queue used instead of external software for simplicity
- Manual SQL chosen over ORM for simplicity and control
- Background worker simulates async event processing pipeline
- Clean separation between API, repository, and worker logic

---

## Project Structure

The project is organized into layers separating API, services, background processing, and data access.

.
├── Program.cs                # Application entry point and endpoint definitions
├── Services/
│   ├── EventRepository.cs    # Data access layer (PostgreSQL via Npgsql)
│   ├── EventQueue.cs         # In-memory asynchronous event queue implementation
│   └── IEventQueue.cs        # Event queue interface
├── Workers/
│   └── EventWorker.cs        # Background worker for async event processing
├── Models/
│   └── Event.cs              # Event data model / DTO
├── db/
|   ├── DbInitializer.cs      # Initializes database schema on startup
│   └── events.sql            # SQL schema definition
├── Dockerfile                # Multi-stage build for containerized app
├── docker-compose.yml        # Runs API + PostgreSQL together

## Future Improvements

- Add message queue (eg. Kafka)
- Implement retries and error handling
- Add filtering and pagination to GET endpoint
- Introduce structured logging
- Write unit tests

---


## Author
Marton Almos Otvos

Change to create example PR
