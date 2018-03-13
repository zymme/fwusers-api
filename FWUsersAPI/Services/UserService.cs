using System;
using System.Linq;

using Microsoft.Extensions.Logging;

using FWUsersAPI.Models;

using FWUsersAPI.Repository;



namespace FWUsersAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userrepository, ILogger<UserService> logger)
        {
            _logger = logger;
            _userRepository = userrepository;
        }

        public UserForCreateDto AddUser(UserForCreateDto user)
        {
            try 
            {
                Console.WriteLine($"Inside AddUser - UserService");

                var createuser = AutoMapper.Mapper.Map<Entities.User>(user);

                _userRepository.AddUser(createuser);

                if(!_userRepository.Save())
                {
                    throw new Exception("Error occurred during Save() of User ...");
                }

                var createdUser = _userRepository.GetCreatedUser();

                if(createuser != null)
                {
                    var createdUserEntry = AutoMapper.Mapper.Map<Models.UserForCreateDto>(createuser);

                    return createdUserEntry;
                }

                return null;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error in AddUser() : {e.Message}");
                throw e;
            }
        }

        public UserDto Authenticate(string username, string password) 
        {
            try
            {
                // call the repository to find user
                var user = _userRepository.AuthenticateUser(username, password);

                if(user != null)
                {
                    var returnuser = AutoMapper.Mapper.Map<UserDto>(user);
                    return returnuser;
                }
                return null;

            }
            catch(Exception e)
            {
                Console.WriteLine($"Error in authenticate user: {e.Message}");
                _logger.LogError($"Error in authenticate user: {e.Message}");
                return null;
            }
        }
    }
}
