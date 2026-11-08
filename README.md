Inventory Management API
 Project Overview
**Inventory Management API** is a backend REST API developed using **ASP.NET Core Web API** for managing products and inventory stock.

The project is designed with a clean layered architecture and provides secure user authentication along with product and stock management functionality.

Features:

-->Product CRUD operations

-->Add and reduce product stock

-->Stock validation

-->User registration and login

-->BCrypt password hashing

-->JWT authentication

-->Protected stock APIs

-->Swagger API documentation

-->Global exception handling

-->Logging

-->Repository and Service layer architecture

-->Unit tests

-->Technology Stack

ASP.NET Core Web API

.NET 10:
  About :
    cross-platform software framework and Long-Term Support (LTS) release from Microsoft, 
    released in November 2025 and supported with updates until November 2028.

Entity Framework Core:
   About:
     Entity Framework Core (EF Core) is an open-source, lightweight, 
     cross-platform object-relational mapper (ORM) developed by Microsoft for .NET applications.

SQL Server:
    About:
      Microsoft SQL Server is a proprietary relational database management system (RDBMS) developed by Microsoft.
      It stores and retrieves data for other applications using Transact-SQL (T-SQL).

JWT Authentication:
     About:
        JSON Web Token (JWT) authentication is an open, stateless standard (RFC 7519) used to securely transmit information between parties as a JSON object. 
        You can experiment with parsing and building tokens

BCrypt:
    About:
      Bcrypt is a slow, adaptive cryptographic password-hashing function designed in 1999 by Niels Provos and David Mazières

Swagger:
   About:
     Swagger is a suite of open-source and commercial tools developed by SmartBear 
     Software that helps developers design, build, document, and test REST APIs.

xUnit:
   About:
     xUnit.net is a free, open-source unit testing framework for C#, F#, and Visual Basic in the 
     .NET ecosystem. Developed by original NUnit creators Brad Wilson and Jim Newkirk, 
     it is part of the .NET Foundation and licensed under Apache 2.0.

Moq:
  About:
   Moq (pronounced "Mock") is the most popular mocking library for C# and .NET. 
   It is used strictly in unit testing to create "mock" objects—simulated test doubles that mimic
   the behavior of real dependencies like databases, APIs, or configuration services. 

Project Structure:

    InventoryManagement.API
    │
    ├── Controllers
    │   ├── AuthController.cs
    │   ├── ProductsController.cs
    │   └── StockController.cs
    │
    ├── Data
    │   └── ApplicationDbContext.cs
    │
    ├── DTOs
    │   ├── LoginRequest.cs
    │   ├── LoginResponse.cs
    │   └── RegisterRequest.cs
    │
    ├── Middleware
    │   └── ExceptionMiddleware.cs
    │
    ├── Models
    │   ├── Product.cs
    │   └── User.cs
    │
    ├── Repositories
    │   ├── IProductRepository.cs
    │   └── ProductRepository.cs
    │
    ├── Services
    │   ├── IAuthService.cs
    │   ├── IProductService.cs
    │   ├── IStockService.cs
    │   ├── AuthService.cs
    │   ├── JwtService.cs
    │   ├── ProductService.cs
    │   └── StockService.cs
    │
    ├── Tests
    │   └── ProductServiceTests.cs
    │
    ├── Program.cs
    └── appsettings.json

How to Run the Project:
    1. Clone the Repository
       Clone this repository from GitHub and open the solution in Visual Studio.
    2. Configure the Database

   Open:
        appsettings.json:
        
          <---- Update the DefaultConnection connection string according to your SQL Server setup. ---->

     3. Create / Update the Database
   
   Open:
   
   Tools → NuGet Package Manager → Package Manager Console
   
   Run:   Update-Database



   3. Run the Application:
     <--  Build → Build Solution  -->


-------------------------------------------------------------------------------------------------------**Open Swagger**------------------------------------------------------------------------------------------------------------------------




**After the application starts, open the Swagger URL shown by Visual Studio.**

Swagger provides the API documentation and allows the endpoints to be tested.




**Authentication**

        POST /api/auth/register
        POST /api/auth/login

**Products**    

        GET    /api/Products
        GET    /api/Products/{id}
        POST   /api/Products
        PUT    /api/Products/{id}
        DELETE /api/Products/{id}
**Stock**

        GET  /api/stock/{productId}
        POST /api/stock/{productId}/add
        POST /api/stock/{productId}/reduce

-------------------------------------------------------------------------------------------------**Unit Tests**-------------------------------------------------------------------------------------------------------------------------------



There Are 5 Unit Tests--->>>


        Get all products
        
        Get product by ID
        
        Create product
        
        Delete product when product does not exist
        
        Update product with mismatched IDs

----------------------------------------------------------------------------------------------**ER Diagram**-----------------------------------------------------------------------------------------------------------------------------------


    
    ┌─────────────────────┐       ┌────────────────────────┐
    │        USER         │       │        PRODUCT         │
    ├─────────────────────┤       ├────────────────────────┤
    │ PK Id               │       │ PK Id                  │
    │ Name                │       │ Name                   │
    │ Email               │       │ Description            │
    │ PasswordHash        │       │ Price                  │
    │ CreatedAt           │       │ StockQuantity          │
    └─────────────────────┘       │ CreatedAt              │
                                  └────────────────────────┘


---------------------------------------------------------------------------------------------------**END**-------------------------------------------------------------------------------------------------------------------------------------





                              

        
