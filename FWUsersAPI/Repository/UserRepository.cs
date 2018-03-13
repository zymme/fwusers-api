using System;
using System.Linq;
using FWUsersAPI.Models;

using FWUsersAPI.Entities;

namespace FWUsersAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;
        private User _user;

        public UserRepository(UserContext usercontext)
        {
            _userContext = usercontext;
        }

        public void AddUser(User user)
        {
            Console.WriteLine($"Adding user ... {user}");
            _user = user;

            var saveresult = _userContext.Add(_user);

        }

        public User GetCreatedUser()
        {
            return _user;
        }

        public bool Save()
        {
            try
            {
                var numreturn = _userContext.SaveChanges();
                Console.WriteLine($"User saved ... {_user}");

                return (numreturn >= 0);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error in save () : {e.Message}");
                return false;
            }
        }


        public User AuthenticateUser(string username, string password)
        {
            var usersearch = _userContext.Users.Where(d => d.username == username &&
                                                      d.pwd == password).FirstOrDefault();

            return usersearch;
        }
    }
}
