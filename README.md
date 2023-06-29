# Hotel Management API

This is a basic REST API for managing hotels and bookings of them.

# Set up

In order to set up the database, please follow the procedure below:
  1. Adjust the connection string according to your SQL Server instance (https://github.com/SotosZarpas86/HotelManagementAPI/blob/master/Anixe/appsettings.json#L3)
  2. Run <update-database> command (on the Package Manager Console) on the Anixe.Infrastructure project (this is where the migration are located)

# Project Architecture Explanation

The solution is implemented using clean architecture. It contains the following projects:

**1. Anixe.Core**
   
      This is the core project of the solution where all the Entities/Models and Abstractions are located.
   
**2. Anixe.Infrastructure**
   
      This project includes the implementation of the repositories and the database migrations.
   
**3. Anixe.Business**
   
      The implementation of the business logic of the API is located in this project.

**4. Anixe.API**
   
      This is the entry project of the solution, and it contains the controllers and a custom exception handler middleware.
   
**5. Anixe.Tests**
   
      The unit tests for the solution can be found in this project.
