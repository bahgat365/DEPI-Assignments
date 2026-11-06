using Microsoft.AspNetCore.Mvc.Filters;

namespace DEPI_ECommerce_API.Filters;

public sealed class LogActionExecutionFilter : IAsyncActionFilter
{
    private readonly ILogger<LogActionExecutionFilter> _logger;

    public LogActionExecutionFilter(ILogger<LogActionExecutionFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var controller = context.RouteData.Values["controller"]?.ToString();
        var action = context.RouteData.Values["action"]?.ToString();

        _logger.LogInformation(
            "Executing action {Controller}.{Action}",
            controller,
            action);

        var executedContext = await next();

        if (executedContext.Exception is not null &&
            !executedContext.ExceptionHandled)
        {
            _logger.LogError(
                executedContext.Exception,
                "Action {Controller}.{Action} failed",
                controller,
                action);
        }
        else
        {
            _logger.LogInformation(
                "Executed action {Controller}.{Action}",
                controller,
                action);
        }
    }
}
