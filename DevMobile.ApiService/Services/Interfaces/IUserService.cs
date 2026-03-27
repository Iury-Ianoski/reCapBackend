using System;
using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Services.Interfaces;

public interface IUserService
{
    Task Register(RegisterDto registerDto);
}
