using MusicSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.Abstract
{
    public interface IAlbumsRepository
    {
        IQueryable<Album> GetAlbumsByPerformerName(string performerName);
        IQueryable<Album> GetAlbumsByPerformerId(string id);
        IQueryable<Album> GetAlbumsBySongTitle(string songTitle);
        IQueryable<Album> GetAlbumsByTitle(string title);
        IQueryable<Album> GetUserAlbumsByUserId(string id);
        IQueryable<Album> GetRecentlyAddedAlbums();
        IQueryable<Album> GetAlbums();
        Album GetAlbumBySongId(string id);
        Album GetAlbumById(string id);
        void SaveAlbum(Album entity);
        void DeleteAlbum(string id);
    }
}
