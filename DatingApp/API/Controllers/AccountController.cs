using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, 
            IMapper mapper){

            _userManager = userManager;

            _mapper = mapper;
            
            _tokenService = tokenService;


        }

        [HttpPost("register")] // POST: api/account/register
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.Username)) return BadRequest("Username is taken");

            var user = _mapper.Map<AppUser>(registerDTO);
            
            // Offloaded responsibility to Identity Framework

            // using var hmac = new HMACSHA512();    
        
            // user.UserName = registerDTO.Username.ToLower();
            // user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));
            // user.PasswordSalt = hmac.Key;

            // _context.Users.Add(user);

            // await _context.SaveChangesAsync();

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if(!result.Succeeded) return BadRequest(result.Errors);

            var roleResults = await _userManager.AddToRoleAsync(user, "Member");
            if (!result.Succeeded) return BadRequest(result.Errors);

            return new UserDTO
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender


                
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == loginDTO.Username);

            if (user == null) return Unauthorized("Invalid username");

            // Offloaded responsibility to Identity Framework

            // using var hmac = new HMACSHA512(user.PasswordSalt);

            // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            // for (int i = 0; i < computedHash.Length; i++){
            //     if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            // }

            var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if(!result) return Unauthorized("Invalid password");

            return new UserDTO
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}