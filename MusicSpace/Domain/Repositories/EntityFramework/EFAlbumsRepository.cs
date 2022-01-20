using Microsoft.EntityFrameworkCore;
using MusicSpace.Domain.Entities;
using MusicSpace.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.EntityFramework
{
    public class EFAlbumsRepository : IAlbumsRepository
    {
        private readonly AppDbContext context;
        public EFAlbumsRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void DeleteAlbum(string id)
        {
            context.Albums.Remove(new Album() { Id = id });
            context.SaveChanges();
        }

        public Album GetAlbumById(string id)
        {
            return context.Albums.FirstOrDefault(e => e.Id == id);
        }

        public Album GetAlbumBySongId(string id)
        {
            return context.Albums.FirstOrDefault(e => e.Songs.Contains(new Song() { Id = id}));
        }

        public IQueryable<Album> GetAlbumsByTitle(string title)
        {
            return context.Albums.Where(e => e.Title == title);
        }

        public IQueryable<Album> GetAlbums()
        {
            return context.Albums;
        }

        public IQueryable<Album> GetAlbumsByPerformerId(string id)
        {
            return context.Albums.Where(e => e.AlbumPerformers.Contains(new AlbumPerformer() { PerformerId = id }));
        }

        public IQueryable<Album> GetAlbumsByPerformerName(string performerName)
        {
            return context.Albums.Where(e => e.AlbumPerformers.Contains(new AlbumPerformer() { Performer = new Performer() { PerformerName = performerName } }));
        }

        public IQueryable<Album> GetAlbumsBySongTitle(string songTitle)
        {
            return context.Albums.Where(e => e.Songs.Contains(new Song() { Title = songTitle }));
        }

        public void SaveAlbum(Album entity)
        {
            if (!context.Albums.Any(e => e.Id == entity.Id))
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IQueryable<Album> GetRecentlyAddedAlbums()
        {
            DateTime date = new DateTime();
            date = DateTime.Now;
            date = date.AddYears(2018 - date.Year);
            return context.Albums.Where(e => e.ReleaseDate < DateTime.Today && e.ReleaseDate >= date);
        }

        public IQueryable<Album> GetUserAlbumsByUserId(string id)
        {
            var userLibraryAlbums = context.UserLibraryAlbums.Where(e => e.UserId == id);
            foreach (var item in userLibraryAlbums)
            {
                item.Album = GetAlbumById(item.AlbumId);
            }

            List<Album> albums = new List<Album>();
            foreach (var item in userLibraryAlbums)
            {
                item.Album.AlbumPerformers = context.AlbumPerformers.Where(e => e.AlbumId == item.AlbumId).ToList();
                albums.Add(item.Album);
            }
            return albums.AsQueryable();
        }
    }
}
