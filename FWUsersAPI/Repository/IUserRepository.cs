using System;

using FWUsersAPI.Models;

using FWUsersAPI.Entities;

namespace FWUsersAPI.Repository
{
    public interface IUserRepository
    {
        void AddUser(User user);

        bool Save();

        User GetCreatedUser();
    }
}
