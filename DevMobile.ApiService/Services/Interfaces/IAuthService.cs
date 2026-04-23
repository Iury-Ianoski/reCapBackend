using System;
using System.Security.Claims;
using DevMobile.ApiService.Dto.User;
using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Services.Interfaces;

public interface IAuthService
{
    Task Register(RegisterDto registerDto);
    Task<UserDto> GetUserFromToken(ClaimsPrincipal userClaims);
}
