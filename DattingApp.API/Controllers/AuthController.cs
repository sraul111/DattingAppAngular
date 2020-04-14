using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DattingApp.API.Data;
using DattingApp.API.DTOs;
using DattingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace DattingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo,IConfiguration config )
       {
            _repo = repo;
            _config = config;
        }

       
       [HttpPost("register")]
       public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto) 
       {
           userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

           if(await _repo.UserExists(userForRegisterDto.Username))
           return BadRequest("Username already exists.");
           var userTocreate = new User
           {
              UserName=userForRegisterDto.Username
           };

           var createdUser = await _repo.Register(userTocreate ,userForRegisterDto.Password);
           return  StatusCode(201);
       }

       [HttpPost("login")]
       public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
       {
           throw new Exception("Computer says no!");
        var userFromRepo =  await _repo.Login(userForLoginDto.Username,userForLoginDto.Password);

        if(userFromRepo==null)
        return Unauthorized();

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
            new Claim(ClaimTypes.Name, userFromRepo.UserName)
        };

        var key =new SymmetricSecurityKey(Encoding.UTF8
        .GetBytes(_config.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Ok(new {token=tokenHandler.WriteToken(token)});
        
       }

    }
}