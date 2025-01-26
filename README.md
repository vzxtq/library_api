# Library API

A RESTful API for managing a library system. This API provides CRUD functionality for books and user management. It allows users to register, log in, and interact with the library's collection of books. The API uses **JWT (JSON Web Tokens)** for secure user authentication.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Installation](#installation)
- [Setup and Configuration](#setup-and-configuration)
- [Usage](#usage)
- [Endpoints](#endpoints)
- [Contributing](#contributing)
- [License](#license)

## Overview

The `Library API` is designed to be a backend service for managing books in a library. It supports basic user authentication and authorization, allowing users to register and log in to access their profiles. The API offers basic CRUD operations for managing books in the library.

## Features

- **User Authentication**:
  - User registration with email and password.
  - User login with JWT authentication.
  - Protected routes for authorized users.

- **Book Management**:
  - Create, Read, Update, and Delete books.
  - Each book has properties like `Title`, `Author`, `Genre`, and `Year`.

- **Secure API Access**:
  - API routes are secured using JWT.
  - Only authenticated users can access protected routes like viewing their profile.

## Technologies Used

- **ASP.NET Core**: The framework used for building the API.
- **Entity Framework Core**: For database access and ORM.
- **SQL Server**: The relational database used to store data.
- **JWT Authentication**: For securing user access.
- **Swagger**: For API documentation and testing.

## Installation

To run the Library API locally, follow these steps:

### Prerequisites

- [Git](https://git-scm.com/downloads)
- [Visual Studio or Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or use a database connection URL for your choice of database)
- [.NET SDK](https://dotnet.microsoft.com/download) (preferably version 6 or higher)

### Clone the Repository

```
git clone https://github.com/vzxtq/library_api.git
cd library_api
```
### Install Dependencies

The required dependencies are defined in the *.csproj files, so just restore them by running:
```
dotnet restore
```
### Set Up Database

1. Open appsettings.json and configure your SQL Server connection string:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "your_connection_string_here"
  }
}
```
2. Run database migrations to set up the database schema:
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```
### Configure JWT

In appsettings.json, define your JWT secret and issuer:
```
{
  "Jwt": {
    "Key": "your_jwt_secret_key_here",
    "Issuer": "your_issuer_here"
  }
}
```
### Setup and Configuration

Once you've cloned the repository and installed dependencies, you'll need to configure the application settings.

1. Database Setup:

- Ensure that your SQL Server is running and accessible.
Run migrations to create the necessary tables.

2. JWT Setup:
- Generate a strong secret key for JWT and update it in appsettings.json.

### Usage
To run the API locally:
```
dotnet run
```
The API will be available at https://localhost:5001 (or another URL based on your configuration).

You can also navigate to https://localhost:5001/swagger to interact with the API via Swagger UI.

### Authentication

To authenticate, send a POST request to /api/auth/login with the following JSON payload:
```
{
  "email": "user@example.com",
  "password": "your_password"
}
```
This will return a JWT token that you can use to access protected endpoints.

### Example Request
```
curl -X GET "https://localhost:5001/api/profile" -H "Authorization: Bearer YOUR_JWT_TOKEN"
```
### Endpoints
#### Authentication
- POST /api/auth/register: Register a new user.
```
{
  "email": "user@example.com",
  "password": "securePassword123",
  "confirmPassword": "securePassword123"
}
```
- POST /api/auth/login: Log in and get a JWT token.
```
{
  "email": "user@example.com",
  "password": "securePassword123"
}
```
#### Books
- GET /api/books: Get all books.

- GET /api/books/{id}: Get a book by ID.

- POST /api/books: Add a new book.

```
{
  "title": "The Great Gatsby",
  "author": "F. Scott Fitzgerald",
  "genre": "Fiction",
  "year": 1925
}
```
- PUT /api/books/{id}: Update an existing book.
```
{
  "title": "The Great Gatsby (Updated)",
  "author": "F. Scott Fitzgerald",
  "genre": "Fiction",
  "year": 1925
}
```
- DELETE /api/books/{id}: Delete a book.
Profile

- GET /api/profile: Get the logged-in user's profile (requires authentication).
```
{
  "Message": "Welcome to your profile!",
  "User": "user@example.com"
}
```
