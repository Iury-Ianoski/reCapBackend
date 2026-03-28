using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Services.Interfaces;
using DevMobile.ApiService.Repositories.Interfaces;


namespace DevMobile.ApiService.Services;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    private readonly IPasswordService _passwordService;

    public UserService(IConfiguration configuration, IUserRepository userRepository, IPasswordService passwordService, IRoleRepository roleRepository)
    {
        _configuration = configuration;
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
}