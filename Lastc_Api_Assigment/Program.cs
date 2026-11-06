using DEPI_ECommerce_API.Extensions;
using DEPI_ECommerce_API.Filters;
using DEPI_ECommerce_API.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    // Apply this Action Filter to all controller actions.
    options.Filters.AddService<LogActionExecutionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ECommerceMappingProfile));

// DbContext, EF Core interceptor, repositories, services and CORS policy.
builder.Services.AddECommerceServices(builder.Configuration);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Exception middleware should be early so it catches downstream exceptions.
app.UseGlobalExceptionHandling();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS must be placed before authentication/authorization and endpoint mapping.
app.UseCors("ECommerceCorsPolicy");

app.UseHttpsRedirection();
app.UseRequestLogging();

app.MapControllers();

app.Run();
