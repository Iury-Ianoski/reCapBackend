using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Services.Interfaces;
using DevMobile.ApiService.Repositories.Interfaces;
using DevMobile.ApiService.Dto.Review;
using DevMobile.ApiService.Dto.Book;
using DevMobile.ApiService.Dto.Genre;
using DevMobile.ApiService.Dto.User;




namespace DevMobile.ApiService.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;



    public ReviewService(IReviewRepository reviewRepository, IBookRepository bookRepository, IUserRepository userRepository)
    {
        _reviewRepository = reviewRepository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<ReviewWithUserDto>> GetLatest(int limit)
    {
        var reviews = await _reviewRepository.GetLatest(limit);

        return reviews.Select(x => new ReviewWithUserDto(
            x.Id, x.Content, x.InitialChapter, x.FinalChapter, x.Spoiler, x.Rating,
            new UserDto(
                x.User.Id,
                x.User.Name,
                x.User.Email
            ),
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

    public async Task<IEnumerable<ReviewWithUserDto>> GetReviewsByUserId(int userId)
    {
        var reviews = await _reviewRepository.GetByUserId(userId);

        return reviews.Select(x => new ReviewWithUserDto(
            x.Id, x.Content, x.InitialChapter, x.FinalChapter, x.Spoiler, x.Rating,
            new UserDto(
                x.User.Id,
                x.User.Name,
                x.User.Email
            ),
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

    public async Task<IEnumerable<ReviewWithUserDto>> GetReviewsByBookId(int bookId)
    {
        var reviews = await _reviewRepository.GetByBookId(bookId);

        return reviews.Select(x => new ReviewWithUserDto(
            x.Id, x.Content, x.InitialChapter, x.FinalChapter, x.Spoiler, x.Rating,
            new UserDto(
                x.User.Id,
                x.User.Name,
                x.User.Email
            ),
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

    

    public async Task<ReviewWithUserDto> GetById(int id)
    {
        var entity = await _reviewRepository.GetWithIncludes(id, r => r.Book, r => r.User);

        if (entity == null)
            return null;

        return new ReviewWithUserDto(
            entity.Id, entity.Content, entity.InitialChapter, entity.FinalChapter, entity.Spoiler, entity.Rating, 
            new UserDto(
                entity.User.Id,
                entity.User.Name,
                entity.User.Email
            ),
            new BookDto(
                entity.Book.Id, 
                entity.Book.Title, 
                entity.Book.Author, 
                entity.Book.PublicationYear, 
                entity.Book.CoverImageUrl ,
                entity.Book.Chapters, 
                entity.Book.Summary,
                entity.Book.Genres.Select(g => new GenreDto(g.Id, g.Name)).ToList())            
        );
    }

    public async Task<ReviewDto> Create(CreateReviewDto dto, int userId)
    {
        var Review = new Review
        {
            Content = dto.Content,
            InitialChapter = dto.InitialChapter,
            FinalChapter = dto.FinalChapter,
            Spoiler = dto.Spoiler,
            Rating = dto.Rating,
            BookId = dto.BookId,
            UserId = userId
        };

        await _reviewRepository.Add(Review);

        var book = await _bookRepository.GetWithIncludes(dto.BookId, b => b.Genres);
        var user = await _userRepository.Get(userId);

        return new ReviewDto(
            Review.Id,
            Review.Content,
            Review.InitialChapter,
            Review.FinalChapter,
            Review.Spoiler,
            Review.Rating,
            Review.UserId,
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