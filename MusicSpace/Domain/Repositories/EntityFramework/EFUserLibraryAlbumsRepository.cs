using Microsoft.EntityFrameworkCore;
using MusicSpace.Domain.Entities;
using MusicSpace.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.EntityFramework
{
    public class EFUserLibraryAlbumsRepository : IUserLibraryAlbumsRepository
    {
        private readonly AppDbContext context;
        public EFUserLibraryAlbumsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void DeleteUserLibraryAlbum(string userId, string albumId)
        {
            context.UserLibraryAlbums.Remove(new UserLibraryAlbum() { UserId = userId, AlbumId = albumId });
            context.SaveChanges();
        }

        public IQueryable<UserLibraryAlbum> GetUserLibraryAlbums()
        {
            return context.UserLibraryAlbums;
        }

        public IQueryable<UserLibraryAlbum> GetUserLibraryAlbumsByUserId(string Id)
        {
            return context.UserLibraryAlbums.Where(e => e.UserId == Id);
        }

        public void SaveUserLibraryAlbum(UserLibraryAlbum entity)
        {
            if (!context.UserLibraryAlbums.Any(e => e.UserId == entity.UserId && e.AlbumId == entity.AlbumId))
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
