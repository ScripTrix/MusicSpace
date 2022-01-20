using MusicSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.Abstract
{
    public interface IApplicationUsersRepository
    {
        IQueryable<ApplicationUser> GetApplicationUsers();
        //ApplicationUser GetApplicationUserByLogin(string login);
        ApplicationUser GetApplicationUserById(string id);
        void SaveApplicationUser(ApplicationUser entity);
        void DeleteApplicationUser(string id);
    }
}
