# Hotel Management API

This is a basic REST api for managing hotels and bookings of them.

# Set up

In order to set up the database, please follow the procedure below:
  1. Adjust the connection string according to your SQL Server instance (https://github.com/SotosZarpas86/HotelManagementAPI/blob/master/Anixe/appsettings.json#L3)
  2. Run <update-database> command (on the Package Manager Console) on the Anixe.Infrastructure project (this is where the migration are located)

# Project Architecture Explanation

The solution is implemented using clean architecture. Itcontains the following projects:

1. Anixe.Core
   This is the core project of the solution where all the Entities/Models and abstractions are located. 
2. Anixe.Infrastructure
3. Anixe.Business
4. Anixe.API
5. Anixe.Tests 
