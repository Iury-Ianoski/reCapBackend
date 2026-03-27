using System;
using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DevMobile.ApiService.Services;

public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<User> _hasher;

    public PasswordService(PasswordHasher<User> hasher)
    {
        _hasher = hasher;
    }

    public async Task<string> HashPassword(User user, string password)
    {
        return _hasher.HashPassword(user, password);
    }

    public async Task<bool> VerifyPassword(User user, string hash, string password)
    {
        var result = _hasher.VerifyHashedPassword(user, hash, password);
        return result == PasswordVerificationResult.Success;
    }
}
