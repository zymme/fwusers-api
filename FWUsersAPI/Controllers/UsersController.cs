using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using FWUsersAPI.Models;
using FWUsersAPI.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FWUsersAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAnyOrigin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userservice)
        {
            _userService = userservice;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return StatusCode(200, new NotImplementedException());
        }

        [HttpPost()]
        [EnableCors("AllowAnyOrigin")]
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
                return StatusCode(500, $"Error in CreateUser() {e.Message}");
            }


        }

    }
}
