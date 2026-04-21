using System;
using DevMobile.ApiService.Dto.Review;

namespace DevMobile.ApiService.Dto.User;

public record UserWithReviewsDto(
    int Id,
    string Name,
    string Email,
    List<ReviewDto> Reviews
);