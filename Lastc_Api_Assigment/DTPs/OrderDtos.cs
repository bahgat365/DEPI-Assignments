namespace DEPI_ECommerce_API.DTOs;

public record OrderCreateDto(DateTime OrderDate, int CustomerId);
public record OrderUpdateDto(DateTime OrderDate, int CustomerId);
public record OrderReadDto(int Id, DateTime OrderDate, int CustomerId, string? CustomerName);
