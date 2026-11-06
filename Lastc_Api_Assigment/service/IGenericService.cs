using DEPI_ECommerce_API.Common;

namespace DEPI_ECommerce_API.Services.Interfaces;

public interface IGenericService<TEntity, TReadDto, TCreateDto, TUpdateDto>
    where TEntity : class
{
    Task<Result<IReadOnlyList<TReadDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<TReadDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<TReadDto>> CreateAsync(TCreateDto dto, CancellationToken cancellationToken = default);
    Task<Result<TReadDto>> UpdateAsync(int id, TUpdateDto dto, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
