using System;

namespace DevMobile.ApiService.Dto.Review;

public record ReviewDto(
    int Id,
    string Content,
    string Chapter,
    bool Spoiler,
    int Rating,
    string Book
);