using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Services.Interfaces;
using DevMobile.ApiService.Repositories.Interfaces;
using DevMobile.ApiService.Dto.Book;
using DevMobile.ApiService.Dto.Genre;
using DevMobile.ApiService.Dto.Review;


namespace DevMobile.ApiService.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IGenreRepository _genreRepository;

    public BookService(IBookRepository bookRepository, IGenreRepository genreRepository)
    {
        _bookRepository = bookRepository;
        _genreRepository = genreRepository;
    }

    public async Task<IEnumerable<BookDto>> GetAll()
    {
        var books = await _bookRepository.GetAllWithIncludes(b => b.Genres, b => b.Reviews);

        return books.Select(b => new BookDto(
            b.Id,
            b.Title,
            b.Author,
            b.PublicationYear,
            b.CoverImageUrl,
            b.Chapters,
            b.Summary,
            b.Genres.Select(g => new GenreDto(g.Id, g.Name)).ToList()
        ));
    }
    

    public async Task<BookDto> GetById(int id)
    {
        var entity = await _bookRepository.GetWithIncludes(id, b => b.Genres);

        if (entity == null)
            return null;

        return new BookDto(
            entity.Id,
            entity.Title,
            entity.Author,
            entity.PublicationYear,
            entity.CoverImageUrl,
            entity.Chapters,
            entity.Summary,
            entity.Genres.Select(g => new GenreDto(g.Id, g.Name)).ToList()
        );
    }

    public async Task<BookDto> Create(CreateBookDto dto)
    {
        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            PublicationYear = dto.PublicationYear,
            CoverImageUrl = dto.CoverImageUrl,
            Chapters = dto.Chapters,
            Summary = dto.Summary
        };

        await _bookRepository.Add(book);

        return new BookDto(
            book.Id,
            book.Title,
            book.Author,
            book.PublicationYear,
            book.CoverImageUrl,
            book.Chapters,
            book.Summary,
            new List<GenreDto>()
        );
    }

    public async Task<bool> Update(int id, UpdateBookDto dto)
    {
        var entity = await _bookRepository.Get(id);

        if (entity == null)
            return false;

        entity.Title = dto.Title;
        entity.Author = dto.Author;
        entity.PublicationYear = dto.PublicationYear;
        entity.CoverImageUrl = dto.CoverImageUrl;
        entity.Chapters = dto.Chapters;
        entity.Summary = dto.Summary;

        _bookRepository.Update(entity);

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _bookRepository.Get(id);

        if (entity == null)
            return false;

        _bookRepository.Delete(entity);

        return true;
    }

    public async Task<bool> AddGenres(int bookId, List<int> genreIds)
    {
        var book = await _bookRepository.GetWithIncludes(bookId, b => b.Genres);

        if (book == null)
            return false;
        
        book.Genres ??= new List<Genre>();

        var genres = await _genreRepository.GetByIds(genreIds);

        foreach (var genre in genres)
        {
            if (!book.Genres.Any(g => g.Id == genre.Id))
            {
                book.Genres.Add(genre);
            }
        }

        _bookRepository.Update(book);

        return true;
    }
}