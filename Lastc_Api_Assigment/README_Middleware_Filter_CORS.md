# Session Assignment — Middleware, Action Filters and CORS

This update adds the session topics to the existing DEPI E-Commerce API.

## 1. Custom Middlewares

### ExceptionHandlingMiddleware
- Catches unhandled exceptions from downstream request processing.
- Logs the exception details on the server.
- Returns a consistent JSON `Result<object>` response with HTTP 500.
- Does not expose internal exception details to API clients.

### RequestLoggingMiddleware
- Measures request execution time.
- Logs HTTP method, path, response status code and elapsed milliseconds.

### Middleware order
`UseGlobalExceptionHandling()` is registered early so it can catch exceptions thrown by later middleware and endpoints. Request logging is registered before endpoint execution so it can record the completed response.

## 2. Action Filter

`LogActionExecutionFilter` implements `IAsyncActionFilter`.
It logs:
- Controller and action before execution.
- Successful action completion.
- Unhandled action exceptions.

It is registered through DI and applied globally using `options.Filters.AddService<LogActionExecutionFilter>()`.

## 3. CORS

A named policy, `ECommerceCorsPolicy`, is configured in `AddECommerceServices`.
Allowed origins are read from `Cors:AllowedOrigins` in `appsettings.json`.

The sample configuration allows common local frontend development origins:
- `http://localhost:4200` / `https://localhost:4200` (Angular)
- `http://localhost:5173` / `https://localhost:5173` (Vite)

For deployment, replace these with the exact production frontend origin(s). Avoid `AllowAnyOrigin()` when credentials or a restricted origin policy is required.

## 4. Run and test

1. Open the solution in Visual Studio.
2. Restore NuGet packages.
3. Run the API.
4. Test existing endpoints in Swagger.
5. To test CORS, send a browser request from an allowed frontend origin. Swagger running on the same API host does not by itself prove browser CORS behavior.
6. To test exception middleware, temporarily throw an exception in a test endpoint/action, call it, verify HTTP 500 and check Visual Studio Output/console logs. Remove the temporary throw after testing.

## Middleware pipeline order
1. Exception handling middleware
2. Swagger (Development only)
3. CORS
4. HTTPS redirection
5. Request logging middleware
6. Controller endpoints

CORS is placed before endpoint mapping so preflight requests can be handled correctly.
