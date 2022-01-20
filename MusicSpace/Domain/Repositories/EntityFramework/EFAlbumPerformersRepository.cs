using Microsoft.EntityFrameworkCore;
using MusicSpace.Domain.Entities;
using MusicSpace.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.EntityFramework
{
    public class EFAlbumPerformersRepository : IAlbumPerformersRepository
    {
        private readonly AppDbContext context;
        public EFAlbumPerformersRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void DeleteAlbumPerformer(string albumId, string performerId)
        {
            context.AlbumPerformers.Remove(new AlbumPerformer() { AlbumId = albumId, PerformerId = performerId });
            context.SaveChanges();
        }

        public IQueryable<AlbumPerformer> GetAlbumPerformersByAlbumId(string id)
        {
            return context.AlbumPerformers.Where(e => e.AlbumId == id);
        }

        public IQueryable<AlbumPerformer> GetAlbumPerformers()
        {
            return context.AlbumPerformers;
        }

        public void SaveAlbumPerformer(AlbumPerformer entity)
        {
            if (!context.AlbumPerformers.Any(e => (e.AlbumId == entity.AlbumId && e.PerformerId == entity.PerformerId)))
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public AlbumPerformer GetAlbumPerformerById(string albumId, string performerId)
        {
            return context.AlbumPerformers.FirstOrDefault(e => e.AlbumId == albumId && e.PerformerId == performerId);
        }
    }
}
