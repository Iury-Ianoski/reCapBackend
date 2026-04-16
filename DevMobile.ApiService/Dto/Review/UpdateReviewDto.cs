using System;

namespace DevMobile.ApiService.Dto.Review;

public record UpdateReviewDto(
    string Content,
    int InitialChapter,
    int? FinalChapter,
    bool Spoiler,
    int Rating,
    int BookId
);
