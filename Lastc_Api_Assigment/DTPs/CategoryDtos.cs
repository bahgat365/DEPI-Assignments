namespace DEPI_ECommerce_API.DTOs;

public record CategoryCreateDto(string Name);
public record CategoryUpdateDto(string Name);
public record CategoryReadDto(int Id, string Name);
