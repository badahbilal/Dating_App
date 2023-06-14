using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Apply the Authorize attribute to restrict access to this action or controller.
    // Only authenticated users will be allowed to access the decorated resource.
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // Apply the AllowAnonymous attribute to allow anonymous access to this action or controller.
        // Unauthenticated users will be permitted to access the decorated resource.
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUsers()
        {
            var users = _context.Users.ToList();

            return users;
        }


        [HttpGet("{id}")]
        public ActionResult<AppUser> GetUser(int id)
        {

            return _context.Users.Find(id);

        }
    }
}