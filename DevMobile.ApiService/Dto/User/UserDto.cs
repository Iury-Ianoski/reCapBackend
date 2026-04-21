using System;
using DevMobile.ApiService.Dto.Review;

namespace DevMobile.ApiService.Dto.User;

public record UserDto(
    int Id,
    string Name,
    string Email
);