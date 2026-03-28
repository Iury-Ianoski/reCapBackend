using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Repositories.Interfaces;
using DevMobile.ApiService.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DevMobile.ApiService.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public TokenService(IConfiguration configuration, IUserRepository userRepository, IPasswordService passwordService)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<string> GenerateToken(LoginDto loginDto)
    {
        var user = await _userRepository.GetByUserName(loginDto.Email);
        
        if(user == null) return String.Empty;

        var validLogin = await _passwordService.VerifyPassword(user, user.Password, loginDto.Password);

        if(!validLogin) return String.Empty;

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
        var issuer = _configuration["Jwt:Issuer"] ?? String.Empty;
        var audience = _configuration["Jwt:Audience"] ?? String.Empty;

        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        // adiciona todas as roles do usuário
        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }

        var tokenOptions = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(40),
            signingCredentials: signinCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return token;
    }
}