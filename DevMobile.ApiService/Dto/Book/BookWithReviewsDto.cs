using DevMobile.ApiService.Dto.Genre;
using DevMobile.ApiService.Dto.Review;

namespace DevMobile.ApiService.Dto.Book;

public record BookWithReviewsDto(
    int Id,
    string Title,
    string Author,
    int PublicationYear,
    string CoverImageUrl,
    int Chapters,
    string Summary,
    List<GenreDto> Genres,
    List<ReviewDto> Reviews
);
