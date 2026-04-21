using System;
using DevMobile.ApiService.Dto.Review;
using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Services.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ReviewWithUserDto>> GetLatest(int limit);
    Task<ReviewWithUserDto> GetById(int id);
    Task<ReviewWithUserDto> Create(CreateReviewDto dto, int userId);
    Task<bool> Update(int id, UpdateReviewDto dto);
    Task<bool> Delete(int id);
}