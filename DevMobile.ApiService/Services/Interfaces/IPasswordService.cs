using System;
using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Services.Interfaces;

public interface IPasswordService
{
    Task<string> HashPassword(User user, string password);
    Task<bool> VerifyPassword(User user, string hash, string password);
}
