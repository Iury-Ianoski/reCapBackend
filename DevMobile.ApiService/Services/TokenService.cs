using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public TokenService(IConfiguration configuration, IUserReoisitory userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<string> GenerateToken(LoginDto loginDto)
    {
        var user = _userRepository.GetByUserName(loginDto.UserName);
        if ()

    }