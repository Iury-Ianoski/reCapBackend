using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Services.Interfaces;
using DevMobile.ApiService.Repositories.Interfaces;
using DevMobile.ApiService.Dto.Review;
using DevMobile.ApiService.Dto.Book;
using DevMobile.ApiService.Dto.Genre;



namespace DevMobile.ApiService.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IBookRepository _bookRepository;


    public ReviewService(IReviewRepository reviewRepository, IBookRepository bookRepository)
    {
        _reviewRepository = reviewRepository;
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<ReviewDto>> GetLatest(int limit)
    {
        var reviews = await _reviewRepository.GetLatest(limit);

        return reviews.Select(x => new ReviewDto(
            x.Id, x.Content, x.InitialChapter, x.FinalChapter, x.Spoiler, x.Rating, 
            new BookDto(
                x.Book.Id, 
                x.Book.Title, 
                x.Book.Author, 
                x.Book.PublicationYear, 
                x.Book.CoverImageUrl ,
                x.Book.Chapters, 
                x.Book.Summary, 
                x.Book.Genres.Select(g => new GenreDto(g.Id, g.Name)).ToList())
        ));
    }

    public async Task<ReviewDto> GetById(int id)
    {
        var entity = await _reviewRepository.GetWithIncludes(id, r => r.Book);

        if (entity == null)
            return null;

        var book = await _bookRepository.GetWithIncludes(entity.Book.Id, b => b.Genres);

        if (book == null)
            return null;

        return new ReviewDto(
            entity.Id, entity.Content, entity.InitialChapter, entity.FinalChapter, entity.Spoiler, entity.Rating, 
            new BookDto(
                book.Id, 
                book.Title, 
                book.Author, 
                book.PublicationYear, 
                book.CoverImageUrl ,
                book.Chapters, 
                book.Summary, 
                book.Genres.Select(g => new GenreDto(g.Id, g.Name)).ToList())
        );
    }

    public async Task<ReviewDto> Create(CreateReviewDto dto)
    {
        var Review = new Review
        {
            Content = dto.Content,
            InitialChapter = dto.InitialChapter,
            FinalChapter = dto.FinalChapter,
            Spoiler = dto.Spoiler,
            Rating = dto.Rating,
            BookId = dto.BookId
        };

        await _reviewRepository.Add(Review);

        var book = await _bookRepository.GetWithIncludes(dto.BookId, b => b.Genres);

        return new ReviewDto(
            Review.Id,
            Review.Content,
            Review.InitialChapter,
            Review.FinalChapter,
            Review.Spoiler,
            Review.Rating,
            new BookDto(
                book.Id, 
                book.Title, 
                book.Author, 
                book.PublicationYear, 
                book.CoverImageUrl ,
                book.Chapters, 
                book.Summary, 
                book.Genres.Select(g => new GenreDto(g.Id, g.Name)).ToList()
            )
        );
    }

    public async Task<bool> Update(int id, UpdateReviewDto dto)
    {
        var entity = await _reviewRepository.Get(id);

        if (entity == null)
            return false;

        entity.Content = dto.Content;
        entity.InitialChapter = dto.InitialChapter;
        entity.FinalChapter = dto.FinalChapter;
        entity.Spoiler = dto.Spoiler;
        entity.Rating = dto.Rating;
        entity.BookId = dto.BookId;

        _reviewRepository.Update(entity);

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _reviewRepository.Get(id);

        if (entity == null)
            return false;

        _reviewRepository.Delete(entity);

        return true;
    }
}