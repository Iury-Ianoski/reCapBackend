using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Services.Interfaces;
using DevMobile.ApiService.Repositories.Interfaces;
using DevMobile.ApiService.Dto.User;
using System.Security.Claims;


namespace DevMobile.ApiService.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    private readonly IPasswordService _passwordService;

    public AuthService(IConfiguration configuration, IUserRepository userRepository, IPasswordService passwordService, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _roleRepository = roleRepository;
    }

    public async Task Register(RegisterDto registerDto)
    {
        var alreadyExists = await _userRepository.GetByUserName(registerDto.Email);
        if (alreadyExists != null)
            return;

        var user = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            Roles = new List<Role>()
        };

        user.Password = await _passwordService.HashPassword(user, registerDto.Password);


        user.CreatedAt = DateTime.UtcNow;

        var initialRole = await _roleRepository.GetByRoleName("Reader");

        user.Roles.Add(initialRole);

        await _userRepository.Add(user);
    }

    public async Task<UserDto> GetUserFromToken(ClaimsPrincipal userClaims)
    {
        var userId = int.Parse(userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var user = await _userRepository.Get(userId);

        if (user == null) return null;

        return new UserDto(
            user.Id,
            user.Name,
            user.Email
        );
    }
}