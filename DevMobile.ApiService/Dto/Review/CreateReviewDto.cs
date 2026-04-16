
namespace DevMobile.ApiService.Dto.Review;

public record CreateReviewDto(
    string Content,
    int InitialChapter,
    int? FinalChapter,
    bool Spoiler,
    int Rating,
    int BookId
);
