using System;
using DevMobile.ApiService.Dto.Book;
using DevMobile.ApiService.Dto.User;

namespace DevMobile.ApiService.Dto.Review;

public record ReviewWithUserDto(
    int Id,
    string Content,
    int InitialChapter,
    int? FinalChapter,
    bool Spoiler,
    int Rating,
    BookDto Book,
    UserDto User
);