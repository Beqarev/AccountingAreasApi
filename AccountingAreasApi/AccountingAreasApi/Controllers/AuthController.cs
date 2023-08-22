using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AccountingAreasApi.Data;
using AccountingAreasApi.Dto;
using AccountingAreasApi.Interfaces;
using AccountingAreasApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace AccountingAreasApi.Controllers;
[Route("Api/authorization")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly DataContext _context;
    private readonly IUserRepository _userRepository;

    public AuthController(IConfiguration configuration, DataContext context, IUserRepository userRepository)
    {
        _configuration = configuration;
        _context = context;
        _userRepository = userRepository;
    }

    public static User user = new User();
    
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto request)
    {
        if (_userRepository.UserExists(request))
            return BadRequest("Username already exists");
        
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        user.Username = request.Username;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        await _userRepository.Add(user);
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDto request)
    {
        // if (!user.Username.Equals(request.Username))
        //     return BadRequest("User not found");

        if (!_userRepository.UserExists(request))
            return BadRequest("User not found");
        
        var userToLogin = _userRepository.GetUserByUsername(request);
        
        if (!VerifyPasswordHash(request.Password, userToLogin.PasswordHash, userToLogin.PasswordSalt))
            return BadRequest("Wrong password");
        
        string token = CreateToken(userToLogin);
        return Ok("bearer " + token);
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        { 
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("JWT:Secret").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims, 
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        return jwt;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}