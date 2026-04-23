using System;
using DevMobile.ApiService.Dto.Book;
using DevMobile.ApiService.Dto.Review;
using DevMobile.ApiService.Dto.User;
using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Services.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> SearchByNamePart(string namePart);
    Task<UserDto> GetById(int id);

}