using MusicSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.Abstract
{
    public interface IUserLibraryAlbumsRepository
    {
        IQueryable<UserLibraryAlbum> GetUserLibraryAlbumsByUserId(string id);
        IQueryable<UserLibraryAlbum> GetUserLibraryAlbums();
        void SaveUserLibraryAlbum(UserLibraryAlbum entity);
        void DeleteUserLibraryAlbum(string userId, string albumId);
    }
}
