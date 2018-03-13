using System;
namespace FWUsersAPI.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public DateTime Birthdate { get; set; }
        public string Username { get; set; }
    }
}
