using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DEPI_ECommerce_API.Data.Interceptors;

public sealed class CommandLoggingInterceptor : DbCommandInterceptor
{
    private readonly ILogger<CommandLoggingInterceptor> _logger;

    public CommandLoggingInterceptor(ILogger<CommandLoggingInterceptor> logger)
    {
        _logger = logger;
    }

    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
        DbCommand command, CommandEventData eventData,
        InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("EF Core SQL Reader command: {CommandText}", command.CommandText);
        return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
    }

    public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(
        DbCommand command, CommandEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("EF Core SQL NonQuery command: {CommandText}", command.CommandText);
        return base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
    }

    public override ValueTask<InterceptionResult< object >> ScalarExecutingAsync(
        DbCommand command, CommandEventData eventData,
        InterceptionResult<object> result,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("EF Core SQL Scalar command: {CommandText}", command.CommandText);
        return base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
    }
}
