using System;
using DevMobile.ApiService.Dto.Genre;
using DevMobile.ApiService.Dto.Review;

namespace DevMobile.ApiService.Dto.User;

public record UserWithRoleDto(
    int Id,
    string Name,
    string Email,
    List<RoleDto> Roles
);