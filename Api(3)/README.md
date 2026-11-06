# DEPI E-Commerce API — EF Core Advanced Assignment

ASP.NET Core 8 Web API implementing the E-commerce assignment and the follow-up session topics.

## Implemented topics
1. **EF Core Configuration classes** — each entity has its own `IEntityTypeConfiguration<T>` class; `ApplyConfigurationsFromAssembly` discovers them.
2. **Seeding** — deterministic sample data is configured with `HasData` for Category, Product, Customer, Order and OrderDetail.
3. **Logging** — built-in `ILogger<T>` logs important CRUD operations and failures; Console and Debug providers are enabled.
4. **Pagination** — `pageNumber` and `pageSize` query parameters; page size is capped at 50.
5. **Generic Result Design Pattern** — consistent `Result<T>` success/failure response wrapper.
6. **Searching** — Category name, Customer name/email, OrderDetail product name.
7. **Filtering** — Orders by customer/date range; OrderDetails by order/product; Category and Customer searches.
8. **AutoMapper** — DTO/entity mapping is configured in `ECommerceMappingProfile`.

## Assignment entity CRUD scope
The first entity in the original E-commerce assignment is Product, so Product remains mapped and seeded but intentionally has no CRUD controller. Full CRUD is provided for Category, Customer, Order, and OrderDetail.

## Relationships
- Category 1-to-many Product
- Customer 1-to-many Order
- Order many-to-many Product through OrderDetail
- OrderDetail composite primary key: `(OrderId, ProductId)`

## Requirements
- .NET 8 SDK
- SQL Server LocalDB (or edit the connection string)
- Visual Studio 2022

## Run using Visual Studio
1. Open `DEPI_ECommerce_API.csproj`.
2. Restore NuGet packages.
3. Open **Tools > NuGet Package Manager > Package Manager Console**.
4. Run:
   ```powershell
   Add-Migration InitialCreate
   Update-Database
   ```
5. Run the project and open the Swagger URL.

## Example query strings

- Categories: `/api/categories?pageNumber=1&pageSize=10&search=Elect`
- Customers: `/api/customers?pageNumber=1&pageSize=5&search=ahmed`
- Orders: `/api/orders?pageNumber=1&pageSize=10&customerId=1&dateFrom=2026-01-01&dateTo=2026-12-31`
- Order details: `/api/orderdetails?pageNumber=1&pageSize=10&orderId=1&productId=1&search=Mouse`

## CRUD endpoints

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

### OrderDetails
- GET `/api/orderdetails`
- GET `/api/orderdetails/{orderId}/{productId}`
- POST `/api/orderdetails`
- PUT `/api/orderdetails/{orderId}/{productId}`
- DELETE `/api/orderdetails/{orderId}/{productId}`

## Notes
- Sample records are inserted through EF Core migrations (`HasData`).
- Seeded IDs are fixed; avoid inserting conflicting explicit IDs.
- Product records are seeded because Product CRUD is excluded in the assignment scope.
- `OrderDetail` read DTO includes ProductName; its Create response may initially return null for ProductName because the navigation is not loaded after insert. Fetch the resource again to see the related name.
