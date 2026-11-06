# DEPI E-Commerce Web API

ASP.NET Core 8 Web API using EF Core, DTOs, Navigation Properties and asynchronous CRUD operations.

## Assignment interpretation
The first entity listed in the E-commerce project is **Product**. Therefore, Product is modeled for relationships and database mapping, but no Product CRUD controller/endpoints are implemented. Full CRUD is implemented for:
- Category
- Customer
- Order
- OrderDetail

## Relationships
- Category 1 --- * Product
- Customer 1 --- * Order
- Order * --- * Product through OrderDetail
- OrderDetail has a composite primary key: (OrderId, ProductId)

## Requirements
- .NET 8 SDK
- SQL Server LocalDB (or change the connection string in `appsettings.json`)
- Visual Studio 2022

## Run
1. Open `DEPI_ECommerce_API.csproj` in Visual Studio.
2. Restore NuGet packages.
3. Open Tools > NuGet Package Manager > Package Manager Console.
4. Run:
   `Add-Migration InitialCreate`
5. Run:
   `Update-Database`
6. Run the project and open the Swagger URL shown by Visual Studio.

## Endpoints

### Categories
- GET `/api/categories`
- GET `/api/categories/{id}`
- POST `/api/categories`
- PUT `/api/categories/{id}`
- DELETE `/api/categories/{id}`

### Customers
- GET `/api/customers`
- GET `/api/customers/{id}`
- POST `/api/customers`
- PUT `/api/customers/{id}`
- DELETE `/api/customers/{id}`

### Orders
- GET `/api/orders`
- GET `/api/orders/{id}`
- POST `/api/orders`
- PUT `/api/orders/{id}`
- DELETE `/api/orders/{id}`

### OrderDetails (composite key)
- GET `/api/orderdetails`
- GET `/api/orderdetails/{orderId}/{productId}`
- POST `/api/orderdetails`
- PUT `/api/orderdetails/{orderId}/{productId}`
- DELETE `/api/orderdetails/{orderId}/{productId}`

All database operations use EF Core asynchronous methods such as `ToListAsync`, `FirstOrDefaultAsync`, `AnyAsync`, `FindAsync`, and `SaveChangesAsync`.

## Important
Create at least one Category, one Customer, one Order, and one Product row in the database before testing OrderDetails. Product CRUD is intentionally omitted as described above; add sample Product data through SQL Server Object Explorer or a temporary seed script if needed.
