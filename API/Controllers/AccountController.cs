using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {

        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")] // POST : api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            // Check if the user is already registered
            if (await IsUserExistsAsync(registerDto.Username)) return BadRequest("Username is taken");


            // Create a new user with the provided username and password.
            // The password is hashed using the HMACSHA512 algorithm and stored in the PasswordHash property.
            // The HMACSHA512 algorithm takes the password as a byte array and computes its hash value.

            // A new instance of the HMACSHA512 class is created to generate the hash and salt.
            // The salt (hmac.Key) is stored in the PasswordSalt property to be used during password verification.

            // The newly created user is added to the Users collection in the _context DbContext.

            // Finally, the changes are saved to the database using _context.SaveChangesAsync(),
            // and the newly created user is returned.
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return new UserDto{
                Username  = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {

            // Retrieve the user record from the database based on the provided username.
            // If no user is found, return an "Unauthorized" response with the message "Invalid username".
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

            if (user == null) return Unauthorized("Invalid username");

            // Create a new instance of the HMACSHA512 class using the user's stored password salt.
            // The password salt is used to recompute the hash for the entered password.
            using var hmac = new HMACSHA512(user.PasswordSalt);

            // Compute the hash of the entered password using the HMACSHA512 algorithm.
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            // Compare each byte of the computed hash with the corresponding byte of the stored password hash.
            // If any byte differs, return an "Unauthorized" response with the message "Invalid password".
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            // If the comparison succeeds, the login is considered successful, and the user object is returned.
            return new UserDto{
                Username  = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }


        private async Task<bool> IsUserExistsAsync(string username)
        {

            return await _context.Users.AnyAsync(u => u.UserName == username.ToLower());
        }

    }
}