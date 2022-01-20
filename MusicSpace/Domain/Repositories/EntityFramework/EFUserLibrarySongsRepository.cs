using Microsoft.EntityFrameworkCore;
using MusicSpace.Domain.Entities;
using MusicSpace.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.EntityFramework
{
    public class EFUserLibrarySongsRepository : IUserLibrarySongsRepository
    {
        private readonly AppDbContext context;
        public EFUserLibrarySongsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void DeleteUserLibrarySong(string userId, string songId)
        {
            context.UserLibrarySongs.Remove(new UserLibrarySong() { UserId = userId, SongId = songId });
            context.SaveChanges();
        }

        public IQueryable<UserLibrarySong> GetUserLibrarySongs()
        {
            return context.UserLibrarySongs;
        }

        public IQueryable<UserLibrarySong> GetUserLibrarySongsByUserId(string Id)
        {
            return context.UserLibrarySongs.Where(e => e.UserId == Id);
        }

        public void SaveUserLibrarySong(UserLibrarySong entity)
        {
            if (!SongExists(entity.UserId, entity.SongId))
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public bool SongExists(string userId, string songId)
        {
            return context.UserLibrarySongs.Any(e => e.UserId == userId && e.SongId == songId);
        }
    }
}
