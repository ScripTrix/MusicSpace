using MusicSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.Abstract
{
    public interface IUserLibrarySongsRepository
    {
        IQueryable<UserLibrarySong> GetUserLibrarySongsByUserId(string Id);
        IQueryable<UserLibrarySong> GetUserLibrarySongs();
        void SaveUserLibrarySong(UserLibrarySong entity);
        void DeleteUserLibrarySong(string userId, string songId);
        bool SongExists(string userId, string songId);
    }
}
