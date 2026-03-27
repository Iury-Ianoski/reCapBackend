using System;

namespace DevMobile.ApiService.Dto.Review;

public record UpdateReviewDto(
    string Content,
    string Chapter,
    bool Spoiler,
    int Rating,
    string Book
);
