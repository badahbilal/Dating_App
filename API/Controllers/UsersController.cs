using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Apply the Authorize attribute to restrict access to this action or controller.
    // Only authenticated users will be allowed to access the decorated resource.
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        // Apply the AllowAnonymous attribute to allow anonymous access to this action or controller.
        // Unauthenticated users will be permitted to access the decorated resource.
        //[AllowAnonymous]
        // HTTP GET method to retrieve a list of users.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            // Retrieve a list of users from the repository asynchronously.
            var users = await _userRepository.GetMembersAsync();

            // Return the list of 'MemberDto' objects as a successful response (HTTP 200 OK).
            return Ok(users);
        }

        // HTTP GET method to retrieve a specific user by username.
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
        }


        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto){

            var username = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = await _userRepository.GetUserByUsernameAsync(username);

            if(user == null) return NotFound();

            _mapper.Map(memberUpdateDto,user);

            if( await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update");
        }

    }
}