using MbStore.Application.DTOs;
using MbStore.Application.DTOs.Identity;
using MbStore.Application.Interfaces;
using MbStore.Infra.Identity;
using MbStore.Utils.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MbStore.Application.Services;

public class AuthService : IAuthService
{
    private ResponseAuthDto _response;
    private readonly UserManager<UserExtended> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<UserExtended> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
        _response = new ResponseAuthDto();
    }

    public async Task<ResponseAuthDto> LoginDtoAsync(LoginDto loginDto)
    {
        try
        {
            var usuario = await _userManager.FindByNameAsync(loginDto.UserName);

            if (usuario == null)
            {
                _response.Message = "User not found.";
                _response.StatusCode = 400;
                return _response;
            }

            var senhaCorreta = await _userManager.CheckPasswordAsync(usuario, loginDto.Password);
            if (!senhaCorreta)
            {
                _response.Message = "The username or password is invalid.";
                _response.StatusCode = 401;
                return _response;
            }

            var userRoles = await _userManager.GetRolesAsync(usuario);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id),
                new Claim("JWTID", Guid.NewGuid().ToString()),
                new Claim("FirstName", usuario.FirstName),
                new Claim("LastName", usuario.LastName)
            };

            var userName = usuario?.UserName;
            if (userName != null)
            {
                authClaims.Add(new Claim(ClaimTypes.Name, userName));
            }

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = GenerateNewJsonWebToken(authClaims);

            _response.IsValid = true;
            _response.StatusCode = 200;
            _response.Message = token;
            return _response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public async Task<ResponseAuthDto> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            var usuario = await _userManager.FindByNameAsync(registerDto.UserName);

            if (usuario != null)
            {
                _response.Message = "The user already has a registration.";
                _response.StatusCode = 400;
                return _response;
            }

            UserExtended newUser = new UserExtended
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var usuarioCriado = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!usuarioCriado.Succeeded)
            {
                string? errorMessage = "Failed to register user: ";
                foreach (var error in usuarioCriado.Errors)
                {
                    errorMessage += $" # {error.Description}";
                }

                _response.Message = errorMessage;
                _response.StatusCode = 400;
                return _response;
            }

            await _userManager.AddToRoleAsync(newUser, SharedConstants.USER);

            _response.IsValid = true;
            _response.Message = "User registered successfully.";
            _response.StatusCode = 200;
            return _response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public async Task<ResponseAuthDto> MakeAdminAsync(PermissionUserDto permissionUserDto)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(permissionUserDto.UserName);
            if (user == null)
            {
                _response.Message = "User not found.";
                _response.StatusCode = 400;
                return _response;
            }

            var userHaveRole = await _userManager.IsInRoleAsync(user, SharedConstants.ADMIN);

            if (userHaveRole)
            {
                _response.Message = "The user is already an administrator.";
                _response.StatusCode = 400;
                return _response;
            }

            await _userManager.AddToRoleAsync(user, SharedConstants.ADMIN);

            _response.IsValid = true;
            _response.Message = "The user is now an administrator";
            _response.StatusCode = 200;
            return _response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public async Task<ResponseAuthDto> MakeOwnerAsync(PermissionUserDto permissionUserDto)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(permissionUserDto.UserName);
            if (user == null)
            {
                _response.Message = "User not found.";
                _response.StatusCode = 400;
                return _response;
            }

            var userHaveRole = await _userManager.IsInRoleAsync(user, SharedConstants.OWNER);

            if (userHaveRole)
            {
                _response.Message = "The user is already an owner.";
                _response.StatusCode = 400;
                return _response;
            }

            await _userManager.AddToRoleAsync(user, SharedConstants.OWNER);

            _response.IsValid = true;
            _response.Message = "The user is now an owner.";
            _response.StatusCode = 200;
            return _response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public string GenerateNewJsonWebToken(List<Claim> claims)
    {
        var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretKey"]));

        var tokenObject = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
            );

        string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

        return token;
    }

    public async Task<ResponseAuthDto> ChangePassword(ChangePasswordDto changePasswordDto)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(changePasswordDto.UserName);
            if (user == null)
            {
                _response.StatusCode = 400;
                _response.Message = "The username or password is invalid.";
                return _response;
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, changePasswordDto.CurrentPassword);

            if (!passwordValid)
            {
                _response.StatusCode = 401;
                _response.Message = "The username or password is invalid.";
                return _response;
            }

            var resultChangePassword = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.Newpassword);

            if (!resultChangePassword.Succeeded)
            {
                string? errorMessage = "Failed to register user: ";
                foreach (var error in resultChangePassword.Errors)
                {
                    errorMessage += $" # {error.Description}";
                }

                _response.Message = errorMessage;
                _response.StatusCode = 400;
                return _response;
            }

            _response.StatusCode = 200;
            _response.Message = "Password changed successfully.";
            _response.IsValid = true;
            return _response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.InnerException?.Message ?? ex.Message);
        }
    }
}
