# Event Service API (.NET + PostgreSQL)

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

---

## Database

- PostgreSQL running in docker container
- Schema is initialized automatically on application startup

---

## Design Notes

- Uses async/await for non-blocking IO
- Manual SQL with Npgsql for simplicity and control
- Background worker simulates event processing pipeline
- Clean separation between API, repository, and worker logic

---

## Project Structure


## Future Improvements

- Add message queue (eg. Kafka)
- Implement retries and error handling
- Add filtering and pagination to GET endpoint
- Introduce structured logging
- Write unit tests

---

## Author
Marton Almos Otvos