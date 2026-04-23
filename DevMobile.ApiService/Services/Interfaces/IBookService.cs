using System;
using DevMobile.ApiService.Dto.Book;
using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAll();
    Task<BookDto> GetById(int id);
    Task<BookDto> Create(CreateBookDto dto);
    Task<bool> Update(int id, UpdateBookDto dto);
    Task<bool> Delete(int id);
    Task<bool> AddGenres(int bookId, List<int> genreIds);
}