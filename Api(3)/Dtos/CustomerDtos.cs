namespace DEPI_ECommerce_API.DTOs;

public record CustomerCreateDto(string Name, string Email);
public record CustomerUpdateDto(string Name, string Email);
public record CustomerReadDto(int Id, string Name, string Email);
