# E-Commerce Node.js API

## Technologies used in the project:
- Express
- Mongo DB with mongoose library
- Soft Delete Feature
- Restful API
- Logging Middleware
- Exception Middleware
- Validation Middleware with Joi Library
- IOC Service Middleware
- JWT Cookie Authentication with Refresh Token Implementation

## Project Structure
```
- controllers => calls related service for request
- middlewares => middlewares used by express
- models => mongoose models
- routes => routes for endpoints
- services => services that handles the request
- utils => some helper functions and classes
```

## Run the Project
- Clone the project
- Create a .env file and define two variable named
    - DB_CONNECTION => Mongo DB connection string
    - ACCESS_TOKEN_SECRET => a complex string for access token creation
- Open the terminal in the root of the project folder
- Run `npm install` command
- Finally run `node app.js` and test API at `localhost:3200/api/`