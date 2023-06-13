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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {

        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")] // POST : api/account/register
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            // Check if the user is already registered
            if(await IsUserExistsAsync(registerDto.Username)) return BadRequest("Username is taken");


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

            return user;
        }


        private async Task<bool> IsUserExistsAsync(string username){

            return await _context.Users.AnyAsync(u => u.UserName == username.ToLower());
        }

    }
}