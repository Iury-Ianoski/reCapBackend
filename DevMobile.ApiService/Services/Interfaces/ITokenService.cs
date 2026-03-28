using System;
using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Services.Interfaces;

public interface ITokenService
{
    Task<string> GenerateToken(LoginDto loginDto);
}
