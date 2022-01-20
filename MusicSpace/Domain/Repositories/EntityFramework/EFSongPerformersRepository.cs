using Microsoft.EntityFrameworkCore;
using MusicSpace.Domain.Entities;
using MusicSpace.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Domain.Repositories.EntityFramework
{
    public class EFSongPerformersRepository : ISongPerformersRepository
    {
        private readonly AppDbContext context;
        public EFSongPerformersRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void DeleteSongPerformer(string songId, string performerId)
        {
            context.SongPerformers.Remove(new SongPerformer() { SongId = songId, PerformerId = performerId });
            context.SaveChanges();
        }

        public SongPerformer GetSongPerformerById(string songId, string performerId)
        {
            return context.SongPerformers.FirstOrDefault(e => e.SongId == songId && e.PerformerId == performerId);
        }

        public IQueryable<SongPerformer> GetSongPerformers()
        {
            return context.SongPerformers;
        }

        public IQueryable<SongPerformer> GetSongPerformersBySongId(string id)
        {
            return context.SongPerformers.Where(e => e.Song.Id == id);
        }

        public void SaveSongPerformer(SongPerformer entity)
        {
            if (!context.SongPerformers.Any(e => e.SongId == entity.SongId && e.PerformerId == entity.PerformerId))
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
