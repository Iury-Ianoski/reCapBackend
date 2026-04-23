using DevMobile.ApiService.Services.Interfaces;
using DevMobile.ApiService.Repositories.Interfaces;
using DevMobile.ApiService.Dto.User;



namespace DevMobile.ApiService.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IReviewRepository _reviewRepository;


    public UserService(IUserRepository userRepository, IReviewRepository reviewRepository)
    {
        _userRepository = userRepository;
        _reviewRepository = reviewRepository;
    }

    public async Task<List<UserDto>> SearchByNamePart(string namePart)
    {
        var users = await _userRepository.SearchByName(namePart);

        return users.Select(b => new UserDto(
            b.Id,
            b.Name,
            b.Email
        )).ToList();
    }
    
    public async Task<UserDto> GetById(int id)
    {
        var entity = await _userRepository.Get(id);

        if (entity == null)
            return null;

        return new UserDto(
            entity.Id,
            entity.Name,
            entity.Email
        );
    }
}