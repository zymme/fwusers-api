using System;


using FWUsersAPI.Repository;
using FWUsersAPI.Models;


namespace FWUsersAPI.Services
{
    public interface IUserService
    {
        UserForCreateDto AddUser(UserForCreateDto user);
    }
}
