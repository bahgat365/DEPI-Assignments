using AutoMapper;
using DEPI_ECommerce_API.Common;
using DEPI_ECommerce_API.Repositories.Interfaces;
using DEPI_ECommerce_API.Services.Interfaces;

namespace DEPI_ECommerce_API.Services;

public class GenericService<TEntity, TReadDto, TCreateDto, TUpdateDto>
    : IGenericService<TEntity, TReadDto, TCreateDto, TUpdateDto>
    where TEntity : class
{
    private readonly IGenericRepository<TEntity> _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GenericService<TEntity, TReadDto, TCreateDto, TUpdateDto>> _logger;

    public GenericService(
        IGenericRepository<TEntity> repository,
        IMapper mapper,
        ILogger<GenericService<TEntity, TReadDto, TCreateDto, TUpdateDto>> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<TReadDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return Result<IReadOnlyList<TReadDto>>.Success(_mapper.Map<IReadOnlyList<TReadDto>>(entities));
    }

    public async Task<Result<TReadDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return Result<TReadDto>.Failure($"{typeof(TEntity).Name} not found.");

        return Result<TReadDto>.Success(_mapper.Map<TReadDto>(entity));
    }

    public async Task<Result<TReadDto>> CreateAsync(TCreateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _repository.AddAsync(entity, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Created {EntityType}", typeof(TEntity).Name);
        return Result<TReadDto>.Success(_mapper.Map<TReadDto>(entity), "Created successfully.");
    }

    public async Task<Result<TReadDto>> UpdateAsync(int id, TUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return Result<TReadDto>.Failure($"{typeof(TEntity).Name} not found.");

        _mapper.Map(dto, entity);
        _repository.Update(entity);
        await _repository.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Updated {EntityType} with id {EntityId}", typeof(TEntity).Name, id);
        return Result<TReadDto>.Success(_mapper.Map<TReadDto>(entity), "Updated successfully.");
    }

    public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return Result<bool>.Failure($"{typeof(TEntity).Name} not found.");

        _repository.Delete(entity);
        await _repository.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Deleted {EntityType} with id {EntityId}", typeof(TEntity).Name, id);
        return Result<bool>.Success(true, "Deleted successfully.");
    }
}
