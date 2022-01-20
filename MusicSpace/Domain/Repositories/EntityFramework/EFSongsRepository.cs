using Microsoft.EntityFrameworkCore;
using MusicSpace.Domain.Entities;
using MusicSpace.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.EntityFramework
{
    public class EFSongsRepository : ISongsRepository
    {
        private readonly AppDbContext context;
        public EFSongsRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void DeleteSong(string id)
        {
            context.Songs.Remove(new Song() { Id = id });
            context.SaveChanges();
        }

        public async Task<Song> GetSongById(string id)
        {
            var some = await context.Songs.FirstOrDefaultAsync(e => e.Id == id);
            return some;
        }

        public IQueryable<Song> GetSongs()
        {
            return context.Songs;
        }

        public IQueryable<Song> GetSongsByAlbumTitle(string albumTitle)
        {
            return context.Songs.Where(e => e.Album.Title == albumTitle);
        }

        public IQueryable<Song> GetSongsByPerformerName(string performerName)
        {
            return context.Songs.Where(e => e.SongPerformers.Contains(new SongPerformer() { Performer = new Performer() { PerformerName = performerName } }));
        }

        public IQueryable<Song> GetSongsByTitle(string title)
        {
            return context.Songs.Where(e => e.Title == title);
        }

        public void SaveSong(Song entity)
        {
            if (!context.Songs.Any(e => e.Id == entity.Id))
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IQueryable<Song> GetRecentlyAddedSongs()
        {
            DateTime date = new DateTime();
            date = DateTime.Now;
            date = date.AddYears(-4);
            return context.Songs.Where(e => e.ReleaseDate < DateTime.Today && e.ReleaseDate >= date);
        }

        public async Task<IQueryable<Song>> GetUserSongsByUserId(string id)
        {
            var userLibrarySongs = await context.UserLibrarySongs.Where(e => e.UserId == id).ToListAsync();
            foreach(var item in userLibrarySongs)
            {
                item.Song = await GetSongById(item.SongId);
            }

            List<Song> songs = new List<Song>();
            foreach(var item in userLibrarySongs)
            {
                item.Song.SongPerformers = await context.SongPerformers.Where(e => e.SongId == item.SongId).Include(e => e.Performer).ToListAsync();
                songs.Add(item.Song);
            }
            foreach(var song in songs)
            {
                
            }
            return songs.AsQueryable();
        }
    }
}
