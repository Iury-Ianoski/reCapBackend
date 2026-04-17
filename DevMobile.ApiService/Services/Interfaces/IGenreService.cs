using System;
using DevMobile.ApiService.Dto.Genre;
using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Services.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetAll();
    Task<GenreDto> GetById(int id);
    Task<GenreDto> Create(CreateGenreDto dto);
    Task<bool> Update(int id, UpdateGenreDto dto);
    Task<bool> Delete(int id);
}