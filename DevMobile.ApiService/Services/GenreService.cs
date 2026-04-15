using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Services.Interfaces;
using DevMobile.ApiService.Repositories.Interfaces;
using DevMobile.ApiService.Dto.Genre;


namespace DevMobile.ApiService.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _GenreRepository;

    public GenreService(IGenreRepository GenreRepository)
    {
        _GenreRepository = GenreRepository;
    }

    public async Task<IEnumerable<GenreDto>> GetAll()
    {
        var Genres = await _GenreRepository.GetAll();

        return Genres.Select(x => new GenreDto(
            x.Id, x.Name
        ));
    }

    public async Task<GenreDto> GetById(int id)
    {
        var entity = await _GenreRepository.Get(id);

        if (entity == null)
            return null;

        return new GenreDto(
            entity.Id,
            entity.Name
        );
    }

    public async Task<GenreDto> Create(CreateGenreDto dto)
    {
        var Genre = new Genre
        {
            Name = dto.Name,
        };

        await _GenreRepository.Add(Genre);

        return new GenreDto(
            Genre.Id,
            Genre.Name
        );
    }

    public async Task<bool> Update(int id, UpdateGenreDto dto)
    {
        var entity = await _GenreRepository.Get(id);

        if (entity == null)
            return false;

        entity.Name = dto.Name;

        _GenreRepository.Update(entity);

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _GenreRepository.Get(id);

        if (entity == null)
            return false;

        _GenreRepository.Delete(entity);

        return true;
    }
}