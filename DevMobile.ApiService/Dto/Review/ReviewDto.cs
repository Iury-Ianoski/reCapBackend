using System;
using DevMobile.ApiService.Dto.Book;

namespace DevMobile.ApiService.Dto.Review;

public record ReviewDto(
    int Id,
    string Content,
    int InitialChapter,
    int? FinalChapter,
    bool Spoiler,
    int Rating,
    BookDto Book
);