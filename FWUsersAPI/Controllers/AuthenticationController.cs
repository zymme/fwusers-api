using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using Microsoft.Extensions.Logging;

using FWUsersAPI.Models;
using FWUsersAPI.Services;
using FWUsersAPI.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FWUsersAPI.Controllers
{
    [EnableCors("AllowAnyOrigin")]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserService _userService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IUserService userservice)
        {
            _logger = logger;
            _userService = userservice;
        }


        [EnableCors("AllowAnyOrigin")]
        [HttpPost]
        public IActionResult Authenticate(string username, string password)
        {
            try 
            {
                var user = _userService.Authenticate(username, password);

                if(user != null)
                    return Ok(user);

                return NotFound(new { message = $"{username} / password not found" } );

            }
            catch(Exception e)
            {
                Console.WriteLine($"Error in Authenticate() : {e.Message}");
                _logger.LogError($"Error in Authenticate() : {e.Message}");

                return StatusCode(500, e.Message);
            }


        }
    }
}
