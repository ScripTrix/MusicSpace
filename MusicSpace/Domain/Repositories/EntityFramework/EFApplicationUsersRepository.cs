using Microsoft.EntityFrameworkCore;
using MusicSpace.Domain.Entities;
using MusicSpace.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.EntityFramework
{
    public class EFApplicationUsersRepository : IApplicationUsersRepository
    {
        private readonly AppDbContext context;
        public EFApplicationUsersRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void DeleteApplicationUser(string id)
        {
            context.ApplicationUsers.Remove(new ApplicationUser() { Id = id });
            context.SaveChanges();
        }

        public ApplicationUser GetApplicationUserById(string id)
        {
            return context.ApplicationUsers
                .Include(b => b.UserLibrarySongs)
                .Include(b => b.UserLibraryAlbums)
                .FirstOrDefault(e => e.Id == id);
        }

        public IQueryable<ApplicationUser> GetApplicationUsers()
        {
            return context.ApplicationUsers;
        }

        public void SaveApplicationUser(ApplicationUser entity)
        {
            if (!context.ApplicationUsers.Any(e => e.Id == entity.Id))
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
