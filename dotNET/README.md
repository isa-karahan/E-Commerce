# E-Commerce .NET API

## Technologies used in the project:
- MS SQL
- Soft Delete Feature
- Restful API
- CQRS with MediatR Library
- Unit of Work Pattern
- Repository Pattern
- CQRS Pipeline
    - Validation with Fluent Validation
    - Logging
- Decorator Pattern with Scrutor Library 
    - Command Transaction Decorator
    - Query Caching Decorator
- JWT Cookie Authentication with Refresh Token Implementation
- Exception Middleware
- Logging Middleware

## Project Structure
```
- WebAPI => Manages the api endpoints and calls the related service using MediatR
- Business
   - Feature (ex. Accounts)
      - Commands
      - Queries
      - Dtos
      - Utils
- DataAccess => Includes database related parts like UnitOfWork and Entities
- Core => Includes the parts that can be used other projects like middleware etc.
```