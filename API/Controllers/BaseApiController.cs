using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // The [ApiController] attribute is applied to this class to indicate that it is an API controller.
    // This attribute provides several built-in behaviors and conventions for API controllers in ASP.NET Core.
    // It enables automatic model validation, binds request data from various sources, handles model state errors,
    // and returns consistent HTTP status codes and response formats for common scenarios.
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}