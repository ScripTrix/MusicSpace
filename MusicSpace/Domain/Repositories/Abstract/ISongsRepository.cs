using MusicSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.Abstract
{
    public interface ISongsRepository
    {
        IQueryable<Song> GetSongsByPerformerName(string performerName);
        IQueryable<Song> GetSongsByAlbumTitle(string albumTitle);
        IQueryable<Song> GetSongsByTitle(string title);
        Task<IQueryable<Song>> GetUserSongsByUserId(string id);
        IQueryable<Song> GetSongs();
        Task<Song> GetSongById(string id);
        void SaveSong(Song entity);
        void DeleteSong(string id);
        public IQueryable<Song> GetRecentlyAddedSongs();
    }
}
