using MbStore.Application.DTOs.Identity;
using MbStore.Application.Interfaces;
using MbStore.Utils.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MbStore.Api.Controllers;

[Route("/api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // Make Admin
    [HttpPost("MakeAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = SharedConstants.ADMIN)]
    public async Task<IActionResult> MakeAdmin([FromBody] PermissionUserDto permissionDto)
    {
        try
        {
            var makeAdminResult = await _authService.MakeAdminAsync(permissionDto);

            if (makeAdminResult.StatusCode == 400)
                return BadRequest(makeAdminResult);

            return Ok(makeAdminResult);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
        }
    }

    // Make Owner
    [HttpPost("MakeOwner")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = SharedConstants.ADMIN)]
    public async Task<IActionResult> MakeOwner([FromBody] PermissionUserDto permissionDto)
    {
        try
        {
            var makeOwnerResult = await _authService.MakeOwnerAsync(permissionDto);

            if (makeOwnerResult.StatusCode == 400)
                return BadRequest(makeOwnerResult);

            return Ok(makeOwnerResult);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
        }
    }

    // Login
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var loginResult = await _authService.LoginDtoAsync(loginDto);

            if (loginResult.StatusCode == 400)
                return BadRequest(loginResult);

            else if (loginResult.StatusCode == 401)
                return Unauthorized(loginResult);

            return Ok(loginResult);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
        }
    }

    // Register
    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var registroResult = await _authService.RegisterAsync(registerDto);

            if (registroResult.StatusCode == 400)
                return BadRequest(registroResult);

            return Ok(registroResult);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
        }
    }

    // Change Password
    [HttpPost("ChangePassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = SharedConstants.ADMIN)]
    public async Task<IActionResult> Changepassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        try
        {
            var changePasswordResult = await _authService.ChangePassword(changePasswordDto);

            if (changePasswordResult.StatusCode == 400)
                return BadRequest(changePasswordResult);

            if (changePasswordResult.StatusCode == 401)
                return Unauthorized(changePasswordResult);

            return Ok(changePasswordResult);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
        }
    }
}
