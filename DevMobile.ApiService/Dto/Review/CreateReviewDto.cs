
namespace DevMobile.ApiService.Dto.Review;

public record CreateReviewDto(
    string Content,
    string Chapter,
    bool Spoiler,
    int Rating,
    string Book
);
