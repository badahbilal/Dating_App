using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
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
        private readonly IPhotoService _photoService;

        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _photoService = photoService;
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


        // HTTP PUT method to update a user's profile information.
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            // Retrieve the user based on the authenticated username.
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            // Check if the user exists.
            if (user == null) return NotFound();

            // Map the properties of 'memberUpdateDto' to the 'user' entity.
            _mapper.Map(memberUpdateDto, user);

            // Save the updated user information.
            if (await _userRepository.SaveAllAsync()) return NoContent();

            // If the update fails, return a BadRequest response.
            return BadRequest("Failed to update");
        }

        // HTTP POST method to add a photo to a user's profile.
        [HttpPost("add-Photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            // Retrieve the user based on the authenticated username.
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            // Check if the user exists.
            if (user == null) return NotFound();

            // Upload the provided photo to a cloud service (e.g., Cloudinary).
            var result = await _photoService.AddPhotoAsync(file);

            // Check if any errors occurred during the photo upload.
            if (result.Error != null) return BadRequest(result.Error.Message);

            // Create a 'Photo' entity based on the upload result and user.
            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            // Set the 'IsMain' flag for the photo if it's the user's first photo.
            if (user.Photos.Count == 0) photo.IsMain = true;

            // Add the photo to the user's collection of photos.
            user.Photos.Add(photo);

            // Save the updated user information, including the new photo.
            if (await _userRepository.SaveAllAsync())
            {
                // Return a created response with information about the added photo.
                return CreatedAtAction(nameof(GetUser), new { username = user.UserName }, _mapper.Map<PhotoDto>(photo));
            }

            // If there's a problem adding the photo, return a BadRequest response.
            return BadRequest("Problem adding photo");
        }
    }
}