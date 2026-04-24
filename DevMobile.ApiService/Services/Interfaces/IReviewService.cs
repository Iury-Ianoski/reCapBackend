using System;
using DevMobile.ApiService.Dto.Review;
using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Services.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ReviewWithUserDto>> GetLatest(int limit);
    Task<IEnumerable<ReviewWithUserDto>> GetReviewsByUserId(int userId);
    Task<IEnumerable<ReviewWithUserDto>> GetReviewsByBookId(int bookId);
    Task<ReviewWithUserDto> GetById(int id);
    Task<ReviewDto> Create(CreateReviewDto dto, int userId);
    Task<bool> Update(int id, UpdateReviewDto dto);
    Task<bool> Delete(int id);
}