# Architecture additions — Session assignment

This update adds the following patterns to the E-Commerce API:

## 1. EF Core Interceptors
`CommandLoggingInterceptor` logs SQL commands executed by EF Core. It is registered as scoped and attached to the DbContext options.

## 2. IService Extension
`AddECommerceServices()` centralizes infrastructure and application dependency injection:
- DbContext + SQL Server
- EF Core interceptor
- Open generic repository
- Open generic service

Call this extension from `Program.cs` instead of registering the DbContext separately.

## 3. Repository Design Pattern
`IGenericRepository<T>` and `GenericRepository<T>` isolate data access from business/application logic. The repository exposes asynchronous reads, add, update, delete, existence checks and an IQueryable for specialized queries.

## 4. Generic Repository Design Pattern
The repository is open-generic and can serve Category, Customer, Order, and other entities without duplicating basic data-access methods.

## 5. Service Design Pattern
`IGenericService<TEntity,TReadDto,TCreateDto,TUpdateDto>` and `GenericService<...>` hold reusable CRUD application logic, AutoMapper conversion, Result responses, and logging.

## Important integration note
The existing controllers still contain entity-specific validation, pagination, searching and filtering from the previous assignment. The generic repository and service are registered and ready for controller-level adoption. For production architecture, move each controller's CRUD orchestration into entity-specific services and keep controllers as thin HTTP adapters. OrderDetail uses a composite key, so it requires a specialized service/repository method rather than the integer-key generic `GetByIdAsync`.
