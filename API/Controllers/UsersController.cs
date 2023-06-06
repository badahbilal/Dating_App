using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // The [ApiController] attribute is applied to this class to indicate that it is an API controller.
    // This attribute provides several built-in behaviors and conventions for API controllers in ASP.NET Core.
    // It enables automatic model validation, binds request data from various sources, handles model state errors,
    // and returns consistent HTTP status codes and response formats for common scenarios.
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        
        private readonly DataContext _context;

        public UsersController (DataContext context){
            _context = context; 
        }


        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUsers()
        {
            var users = _context.Users.ToList();

            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<AppUser> GetUser(int id){

            return _context.Users.Find(id);

        }
    }
}