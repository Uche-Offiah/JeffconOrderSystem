# Jeffcon Order System

## Overview
This project is a production-grade Order Processing API built using C# and .NET, implementing Clean Architecture and event-driven design.

---

## Architecture

- Domain Layer ? Core business logic
- Application Layer ? Use cases and services
- Infrastructure Layer ? Database and messaging
- API Layer ? HTTP endpoints

---

## Features

- Order creation API
- Validation using FluentValidation
- Global exception handling
- Structured logging with Serilog
- Event-driven architecture using RabbitMQ
- Outbox Pattern for reliable message delivery
- Retry policies using Polly

---

## Tech Stack

- .NET 8
- Entity Framework Core
- RabbitMQ
- Polly
- FluentValidation
- Serilog

---

## How It Works

1. Client sends request to create order
2. Order is saved in database
3. Event is saved in Outbox table
4. Background worker reads Outbox
5. Event is published to RabbitMQ

---

## Running the Project

### 1. Run RabbitMQ

docker run -d -p 5672:5672 -p 15672:15672 rabbitmq:3-management

### 2. Run API
dotnet run --project OrderService.API

### 3. Test endpoint
http://localhost:5000/api/orders
{
  "amount": 100
}