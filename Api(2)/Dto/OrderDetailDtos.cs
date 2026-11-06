namespace DEPI_ECommerce_API.DTOs;

public record OrderDetailCreateDto(int OrderId, int ProductId, int Quantity);
public record OrderDetailUpdateDto(int Quantity);
public record OrderDetailReadDto(int OrderId, int ProductId, int Quantity, string? ProductName);
