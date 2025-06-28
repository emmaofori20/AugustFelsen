using AugustFelsen.Application.DTOs;
using AugustFelsen.Domain.Entities;
using AugustFelsen.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AugustFelsen.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        try
        {
            // Check if user already exists
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "User with this email already exists."
                };
            }

            // Create new user
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                EmailConfirmed = true,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserType = Enum.TryParse<UserType>(request.UserType, true, out var userType) ? userType : UserType.Client
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            // Assign role based on user type
            var role = GetRoleFromUserType(request.UserType);
            await _userManager.AddToRoleAsync(user, role);

            // Generate JWT token
            var token = GenerateJwtToken(user, new List<string> { role });

            return new AuthResponse
            {
                Success = true,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["Jwt:DurationInMinutes"])),
                User = new UserDto
                {
                    Id = user.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = user.Email!,
                    PhoneNumber = user.PhoneNumber,
                    UserType = request.UserType,
                    Roles = new List<string> { role }
                }
            };
        }
        catch (Exception ex)
        {
            return new AuthResponse
            {
                Success = false,
                Message = $"Registration failed: {ex.Message}"
            };
        }
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isValidPassword)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles.ToList());

            return new AuthResponse
            {
                Success = true,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["Jwt:DurationInMinutes"])),
                User = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email!,
                    PhoneNumber = user.PhoneNumber,
                    UserType = GetUserTypeFromRole(roles.FirstOrDefault()),
                    Roles = roles.ToList()
                }
            };
        }
        catch (Exception ex)
        {
            return new AuthResponse
            {
                Success = false,
                Message = $"Login failed: {ex.Message}"
            };
        }
    }

    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
    {
        // Implement refresh token logic here
        throw new NotImplementedException("Refresh token functionality not implemented yet.");
    }

    public async Task<bool> LogoutAsync(string userId)
    {
        // Implement logout logic here (e.g., invalidate refresh token)
        return true;
    }

    private string GenerateJwtToken(User user, List<string> roles)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.UserName!)
        };

        // Add roles to claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["Jwt:DurationInMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GetRoleFromUserType(string userType)
    {
        return userType.ToLower() switch
        {
            "admin" => "Admin",
            "client" => "Client",
            "professional" => "Professional",
            "artisan" => "Artisan",
            "supplier" => "Supplier",
            _ => "Client"
        };
    }

    private string GetUserTypeFromRole(string? role)
    {
        return role?.ToLower() switch
        {
            "admin" => "Admin",
            "client" => "Client",
            "professional" => "Professional",
            "artisan" => "Artisan",
            "supplier" => "Supplier",
            _ => "Client"
        };
    }
} 