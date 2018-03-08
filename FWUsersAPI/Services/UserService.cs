using System;
using System.Linq;
using FWUsersAPI.Models;

using FWUsersAPI.Repository;

namespace FWUsersAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userrepository)
        {
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
    }
}
