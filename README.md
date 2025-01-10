# MassTransit-Demo

This project demonstrates the integration of **MassTransit** with **Amazon SQS** for messaging and **Entity Framework Core** for database operations. It features an API for managing movies and user mappings with a focus on distributed messaging patterns.

---

## Directory Structure

```plaintext
MassTransit-Demo/
├── README.md
├── MassTransit-SQS/
│   ├── MassTransit-SQS.sln
│   ├── docker-compose.yml
│   ├── .dockerignore
│   ├── .gitignore
│   ├── Message.Contracts/
│   │   ├── Message.Contracts.csproj
│   │   └── UpdateUserMovieMapping.cs
│   └── Movies.Api/
│       ├── AppContext.cs
│       ├── Dockerfile
│       ├── Movies.Api.csproj
│       ├── Movies.Api.http
│       ├── Program.cs
│       ├── appsettings.Development.json
│       ├── appsettings.json
│       ├── Configuration/
│       │   ├── DataProviderConfiguration.cs
│       │   ├── EndPointConfiguration.cs
│       │   └── MessageBrokerConfiguration.cs
│       ├── Consumers/
│       │   └── UserMovieMappingConsumer.cs
│       ├── Controllers/
│       │   └── MovieController.cs
│       ├── DTOs/
│       │   └── MovieDto.cs
│       ├── Entity/
│       │   ├── Movie.cs
│       │   ├── User.cs
│       │   └── UserMovieMapping.cs
│       ├── Enums/
│       │   └── UserType.cs
│       ├── Extension/
│       │   └── MigrationExtension.cs
│       ├── Mapper/
│       │   └── MovieMapper.cs
│       ├── Migrations/
│       └── Properties/
│           └── launchSettings.json
└── .github/
    └── workflows/
        └── main.yml
```

---

## Features

- **MassTransit** integration with Amazon SQS for message-based communication.
- Entity Framework Core for database management.
- Dockerized setup for API and message broker.
- REST API to manage movies and user mappings.

---

## Prerequisites

- .NET SDK 8.0 or later
- Docker
- Amazon SQS

---

## Getting Started

### Clone the Repository
```bash
git clone https://github.com/shubhambisht541/MassTransit-Demo.git
cd MassTransit-Demo/MassTransit-SQS
```

### Run with Docker Compose

1. Ensure Docker is running.
2. Use the `docker-compose.yml` file to spin up the services.

```bash
docker-compose up -d
```

### Configuration

Update the `appsettings.json` and `appsettings.Development.json` files to include your AWS credentials, database connection strings, and other required configurations.

### Build and Run

1. Build the solution:
   ```bash
   dotnet build MassTransit-SQS.sln
   ```
2. Navigate to the `Movies.Api` directory and run the API:
   ```bash
   cd Movies.Api
   dotnet run
   ```

---

## Endpoints

### MovieController
- **GET /api/movies**: Fetch all movies.
- **POST /api/movies**: Add a new movie.

### Messaging
- **UserMovieMappingConsumer**: Listens to messages about user-movie mapping updates and processes them.

---

## Project Details

### Movies.Api

- **AppContext.cs**: Handles the Entity Framework Core DB context.
- **Controllers/**: Contains the API endpoints.
- **Consumers/**: Includes the `UserMovieMappingConsumer` class for processing messages.
- **Mapper/**: Responsible for mapping between entities and DTOs.

### Message.Contracts

- Defines the message structure shared between the producer and consumer.
- **UpdateUserMovieMapping.cs**: Contract for user-movie mapping updates.

### Migrations

Contains the EF Core migration files for database schema updates.

---

## CI/CD

The `.github/workflows/main.yml` file sets up a CI pipeline to build the project.

---

## Authors

- **Shubham Bisht** - [GitHub Profile](https://github.com/shubhambisht541)

---
