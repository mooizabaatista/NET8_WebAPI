using MbStore.Application.DTOs.Identity;
using MbStore.Application.DTOs;
using System.Security.Claims;

namespace MbStore.Application.Interfaces;

public interface IAuthService
{
    Task<ResponseAuthDto> MakeAdminAsync(PermissionUserDto permissionUserDto);
    Task<ResponseAuthDto> MakeOwnerAsync(PermissionUserDto permissionUserDto);
    Task<ResponseAuthDto> RegisterAsync(RegisterDto registerDto);
    Task<ResponseAuthDto> LoginDtoAsync(LoginDto loginDto);
    Task<ResponseAuthDto> ChangePassword(ChangePasswordDto changePasswordDto);
    public string GenerateNewJsonWebToken(List<Claim> claims);
}
