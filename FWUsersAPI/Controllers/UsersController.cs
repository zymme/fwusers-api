using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using Microsoft.Extensions.Logging;

using FWUsersAPI.Models;
using FWUsersAPI.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FWUsersAPI.Controllers
{
    [EnableCors("AllowAnyOrigin")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userservice, ILogger<UsersController> logger)
        {
            _userService = userservice;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return StatusCode(200, new NotImplementedException());
        }


        [EnableCors("AllowAnyOrigin")]
        [HttpPost()]
        public IActionResult CreateUser([FromBody] UserForCreateDto user)
        {
            try 
            {
                var createduser = _userService.AddUser(user);

                if(createduser == null)
                {
                    return Ok(new { status = "Error", message = "Null object returned from create user, check logs for issue"} );
                }

                return Ok(createduser);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error in CreateUser() {e.Message}");
                _logger.LogError($"Error in CreateUser() {e.Message} ");

                return StatusCode(500, $"Error in CreateUser() {e.Message}");
            }


        }


    }
}
