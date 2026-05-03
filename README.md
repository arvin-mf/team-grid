# Project Name

## _**TeamGrid**_

TeamGrid is a streamlined team management platform designed to automate the fair and randomized distribution of teams into sessions. By leveraging a balanced-load algorithm, the system ensures that every session maintains a proportional number of participants with minimal variance, providing an efficient and transparent scheduling solution for course coordinators.

# Getting Started

## .NET SDK
Ensure you have the latest .NET SDK installed on your system. You can download it from [the official Microsoft .NET website](https://dotnet.microsoft.com/en-us/download).

## Docker
Docker and Docker Compose are required to manage and run the containerized services. Make sure Docker Desktop or the Docker daemon is running.

## Installation & Setup

### 1. Clone the repository
Clone the repository to your local machine using your terminal:

```
https://github.com/arvin-mf/team-grid.git
cd team-grid
```

### 2. Prepare environment variables
Create a `.env` file in the root directory to store your environment variables and connection strings. You can base it on the provided example: `.env.example`

```
cp .env.example .env
```

# How to Run

This project uses a `Makefile` to simplify development and deployment tasks. Ensure you have 'make' installed on your system. Otherwise, you can use the original commands written in the `Makefile`.

## Deployment with Docker
To spin up the database container in detached mode:

```
make compose-up
```

## Database Migrations
To apply the latest database schema migrations:

```
make migrate
```

## Starting the Application
To run the API application directly:

```
make app
```

---
Other commands available in `Makefile`

```
make compose-logs-db
```

```
make compose-down
```

```
make test
```

# API Documentation

The API endpoints are fully documented in a Postman collection. You can explore the available routes, request parameters, and example responses by visiting the link below:

[View Postman Documentation](https://documenter.getpostman.com/view/47592718/2sBXqKofcY)

---
