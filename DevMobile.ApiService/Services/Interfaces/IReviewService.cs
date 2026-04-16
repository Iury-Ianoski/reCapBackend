using System;
using DevMobile.ApiService.Dto.Review;
using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Services.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetLatest(int limit);
    Task<ReviewDto> GetById(int id);
    Task<ReviewDto> Create(CreateReviewDto dto);
    Task<bool> Update(int id, UpdateReviewDto dto);
    Task<bool> Delete(int id);
}