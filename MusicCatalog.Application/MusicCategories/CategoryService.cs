using MusicCatalog.Domain;
using MusicCatalog.Domain.Models;

namespace MusicCatalog.Application.MusicCategories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<int> CreateAsync(MusicGenre genre, CancellationToken cancellationToken)
    {
        return await _categoryRepository.AddAsync(genre, cancellationToken);
    }

    public async Task UpdateAsync(MusicGenre genre, CancellationToken cancellationToken)
    {
        await _categoryRepository.UpdateAsync(genre, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _categoryRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<MusicGenre> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<MusicGenre>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetAllAsync(dto, cancellationToken);
    }
}